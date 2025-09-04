namespace SFMovies.Application.DTOs
{
    public class MovieLocationDto
    {
        public string Address { get; init; } = string.Empty;
        public string? FunFact { get; init; }
        public double? Latitude { get; init; }
        public double? Longitude { get; init; }
    }
}
