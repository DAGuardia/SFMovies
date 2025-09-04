using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFMovies.Application.Interfaces;
using SFMovies.Application.Services;
using SFMovies.Infrastructure.ExternalApi;

namespace SFMovies.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient<IDataSFClient, DataSFClient>(client =>
        {
            client.BaseAddress = new Uri("https://data.sfgov.org/");
            client.Timeout = TimeSpan.FromSeconds(30);

            var token = config["Socrata:AppToken"];
            if (!string.IsNullOrWhiteSpace(token))
                client.DefaultRequestHeaders.Add("X-App-Token", token);
        });

        return services;
    }
}