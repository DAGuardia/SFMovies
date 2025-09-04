namespace SFMovies.Application.DTOs
{
    public class MovieDto
    {
        public string Title { get; init; } = string.Empty;
        public int? ReleaseYear { get; init; }
        public string? ProductionCompany { get; init; }
        public string? Director { get; init; }
        public string? Writer { get; init; }
        public IReadOnlyList<string> Cast { get; init; } = Array.Empty<string>();
        public IReadOnlyList<MovieLocationDto> Locations { get; init; } = Array.Empty<MovieLocationDto>();
    }
}
