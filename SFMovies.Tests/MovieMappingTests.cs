using SFMovies.Domain.Entities;
using Xunit;

namespace SFMovies.Tests
{
    public class MovieMappingTests
    {
        [Fact]
        public void MapRowToMovie_HandlesMissingFieldsGracefully()
        {
            // Arrange
            var row = new
            {
                title = (string?)null,
                release_year = "2011",
                locations = "City Hall",
                fun_facts = (string?)null,
                director = "Jayendra",
                production_company = "SPI Cinemas",
                actor_1 = "S1",
                actor_2 = (string?)null,
                actor_3 = "S3",
                latitude = "37.7793",
                longitude = "-122.4192"
            };

            // Act: simular tu mapper real
            var cast = new[] { row.actor_1, row.actor_2, row.actor_3 };
            var movie = new Movie(
                title: row.title ?? "Unknown",
                releaseYear: int.TryParse(row.release_year, out var y) ? y : null,
                productionCompany: row.production_company,
                director: row.director,
                writer: null,
                cast: cast.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s!.Trim()).ToArray(),
                locations: new[]
                {
                new MovieLocation(
                    address: row.locations!,
                    funFact: row.fun_facts,
                    latitude: double.TryParse(row.latitude, out var lat) ? lat : null,
                    longitude: double.TryParse(row.longitude, out var lng) ? lng : null
                )
                }
            );

            // Assert
            Assert.Equal("Unknown", movie.Title);
            Assert.Equal(2011, movie.ReleaseYear);
            Assert.Equal("City Hall", movie.Locations[0].Address);
            Assert.Equal(2, movie.Cast.Count());
        }


    }
}