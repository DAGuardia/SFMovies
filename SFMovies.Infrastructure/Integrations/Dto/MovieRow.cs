using System.Text.Json.Serialization;

namespace SFMovies.Infrastructure.Integrations.Dto
{

    public class MovieRow
    {
        [JsonPropertyName("title")] public string? Title { get; init; }
        [JsonPropertyName("locations")] public string? Locations { get; init; }
        [JsonPropertyName("fun_facts")] public string? FunFacts { get; init; }
        [JsonPropertyName("release_year")] public string? ReleaseYear { get; init; }
        [JsonPropertyName("production_company")] public string? ProductionCompany { get; init; }
        [JsonPropertyName("director")] public string? Director { get; init; }
        [JsonPropertyName("writer")] public string? Writer { get; init; }
        [JsonPropertyName("actor_1")] public string? Actor1 { get; init; }
        [JsonPropertyName("actor_2")] public string? Actor2 { get; init; }
        [JsonPropertyName("actor_3")] public string? Actor3 { get; init; }
        [JsonPropertyName("latitude")] public string? Latitude { get; init; }
        [JsonPropertyName("longitude")] public string? Longitude { get; init; }
        [JsonPropertyName("location")] public MovieLocationObject? Location { get; init; }

        public class MovieLocationObject
        {
            [JsonPropertyName("latitude")] public string? Latitude { get; init; }
            [JsonPropertyName("longitude")] public string? Longitude { get; init; }
            [JsonPropertyName("human_address")] public string? HumanAddress { get; init; }
        }
    }
}