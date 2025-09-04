using Microsoft.Extensions.Caching.Memory;
using SFMovies.Application.DTOs;
using SFMovies.Application.Interfaces;
using SFMovies.Application.Mappers;
using System.Collections.Generic;

namespace SFMovies.Application.Services
{
    public class MovieService : IMovieService
    {
        public MovieService(IDataSFClient dataSFClient, IMemoryCache cache)
        {
            _client = dataSFClient;
            _cache = cache;
        }

        private IDataSFClient _client { get; }
        private IMemoryCache _cache { get; }

        public async Task<IEnumerable<MovieDto>> GetAll(string? title = null)
        {
            var movies = await _client.GetMoviesAsync(title);
            return movies.ToDtoList();
        }

        public async Task<List<TittleSuggestionDto>> SuggestTitlesAsync(string prefix, int limit = 10, CancellationToken ct = default)
        {
            prefix = (prefix ?? string.Empty).Trim();
            if (prefix.Length < 2) return new List<TittleSuggestionDto>();

            var cacheKey = $"sugg:{prefix}:{limit}";
            if (_cache.TryGetValue(cacheKey, out List<TittleSuggestionDto>? cached))
                return cached!;

            var titles = await _client.SuggestTitlesAsync(prefix, limit, ct);
            var result = titles.Select(t => new TittleSuggestionDto { Value = t }).ToList();

            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
            return result;
        }
    }
}
