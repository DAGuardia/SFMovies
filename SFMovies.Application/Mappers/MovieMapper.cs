using SFMovies.Application.DTOs;
using SFMovies.Domain.Entities;

namespace SFMovies.Application.Mappers;

public static class MovieMapper
{
    public static MovieDto ToDto(this Movie movie)
    {
        return new MovieDto
        {
            Title = movie.Title,
            ReleaseYear = movie.ReleaseYear,
            ProductionCompany = movie.ProductionCompany,
            Director = movie.Director,
            Writer = movie.Writer,
            Cast = movie.Cast.ToArray(),
            Locations = movie.Locations
                .Select(l => new MovieLocationDto
                {
                    Address = l.Address,
                    FunFact = l.FunFact,
                    Latitude = l.Latitude,
                    Longitude = l.Longitude
                })
                .ToArray()
        };
    }

    public static List<MovieDto> ToDtoList(this List<Movie> movies)
        => movies.Select(m => m.ToDto()).ToList();
}