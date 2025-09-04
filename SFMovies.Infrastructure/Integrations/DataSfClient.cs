using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using SFMovies.Application.Interfaces;
using SFMovies.Application.Services;
using SFMovies.Domain.Entities;
using SFMovies.Infrastructure.ExternalApi.Dto;
using SFMovies.Infrastructure.Integrations.Dto;
using SFMovies.Infrastructure.Integrations.Mappers;
using System.Data;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;

namespace SFMovies.Infrastructure.ExternalApi;

public class DataSFClient : IDataSFClient
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;
    private readonly string _resource = "api/v3/views/yitu-d5am/query.json";

    public DataSFClient(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
    }

    public async Task<List<Movie>> GetMoviesAsync(string? title = null, int limit = 100, int offset = 0, CancellationToken ct = default)
    {
        var columns = new[]
        {
        "title","locations","fun_facts","release_year",
        "production_company","director","writer",
        "actor_1","actor_2","actor_3","latitude","longitude"
        };

        var query = new Dictionary<string, string?>(StringComparer.Ordinal)
        {
            ["$select"] = string.Join(",", columns),
            ["$order"] = "title, release_year",
            ["$limit"] = Math.Clamp(limit, 1, 500).ToString(CultureInfo.InvariantCulture),
            ["$offset"] = Math.Max(0, offset).ToString(CultureInfo.InvariantCulture)
        };

        if (!string.IsNullOrWhiteSpace(title))
        {
            var safe = title.Replace("'", "''", StringComparison.Ordinal);
            query["$where"] = $"upper(title) like upper('%{safe}%')";
        }

        var baseUri = new Uri(_http.BaseAddress!, _config["Socrata:ResourcePath"]);
        var url = QueryHelpers.AddQueryString(baseUri.ToString(), query);

        using var resp = await _http.GetAsync(url, ct);
        resp.EnsureSuccessStatusCode();

        var json = await resp.Content.ReadAsStringAsync(ct);
        var rows = JsonSerializer.Deserialize<List<MovieRow>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? [];   
        
        // Map a entidad de dominio
        return rows.ToDomainGrouped();
    }
    public async Task<List<string>> SuggestTitlesAsync(string prefix, int limit, CancellationToken ct = default)
    {
        var ub = new UriBuilder(new Uri(_http.BaseAddress!, _config["Socrata:ResourcePath"]));
        var q = HttpUtility.ParseQueryString(ub.Query);

        var safe = prefix.Replace("'", "''");
        q["$select"] = "title";
        q["$where"] = $"upper(title) like upper('%{safe}%')";
        q["$group"] = "title";
        q["$order"] = "title";
        q["$limit"] = Math.Clamp(limit, 1, 50).ToString();

        ub.Query = q.ToString();

        using var resp = await _http.GetAsync(ub.Uri, ct);
        resp.EnsureSuccessStatusCode();

        var json = await resp.Content.ReadAsStringAsync(ct);
        var rows = JsonSerializer.Deserialize<List<TitleRow>>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new();

        return rows.Where(r => !string.IsNullOrWhiteSpace(r.Title))
                   .Select(r => r.Title!.Trim())
                   .Distinct(StringComparer.OrdinalIgnoreCase)
                   .ToList();
    }
}
