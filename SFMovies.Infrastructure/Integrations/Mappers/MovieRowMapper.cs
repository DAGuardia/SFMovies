using SFMovies.Domain.Entities;
using SFMovies.Infrastructure.Integrations.Dto;

namespace SFMovies.Infrastructure.Integrations.Mappers;

internal static class MovieRowMapper
{
    public static Movie ToDomain(this MovieRow r)
    {
        double? lat = TryParseDouble(r.Latitude) ?? TryParseDouble(r.Location?.Latitude);
        double? lng = TryParseDouble(r.Longitude) ?? TryParseDouble(r.Location?.Longitude);

        var cast = new[] { r.Actor1, r.Actor2, r.Actor3 }
            .Where(s => !string.IsNullOrWhiteSpace(s))!
            .Select(s => s!.Trim())
            .ToArray();

        var locations = string.IsNullOrWhiteSpace(r.Locations)
            ? Array.Empty<MovieLocation>()
            : new[] { new MovieLocation(
                address: r.Locations!.Trim(),
                funFact: string.IsNullOrWhiteSpace(r.FunFacts) ? null : r.FunFacts!.Trim(),
                latitude: lat,
                longitude: lng
              )};

        return new Movie(
            title: r.Title ?? "Unknown",
            releaseYear: int.TryParse(r.ReleaseYear, out var parsed) ? parsed : null,
            productionCompany: r.ProductionCompany,
            director: r.Director,
            writer: r.Writer,
            cast: cast,
            locations: locations
        );
    }

    public static List<Movie> ToDomainGrouped(this IEnumerable<MovieRow> rows)
    {
        return rows
            .Where(r => !string.IsNullOrWhiteSpace(r.Title))
            .GroupBy(r => new
            {
                Title = r.Title!.Trim(),
                Year = ParseInt(r.ReleaseYear)
            })
            .Select(g =>
            {
                // Tomo una fila como fuente de metadatos (director, writer, etc.)
                var first = g.First();

                var cast = g.SelectMany(r => new[] { r.Actor1, r.Actor2, r.Actor3 })
                            .Where(s => !string.IsNullOrWhiteSpace(s))
                            .Select(s => s!.Trim())
                            .Distinct(StringComparer.OrdinalIgnoreCase)
                            .ToArray();

                var locations = g
                    .Where(r => !string.IsNullOrWhiteSpace(r.Locations))
                    .Select(r =>
                    {
                        var lat = TryParseDouble(r.Latitude) ?? TryParseDouble(r.Location?.Latitude);
                        var lng = TryParseDouble(r.Longitude) ?? TryParseDouble(r.Location?.Longitude);
                        return new MovieLocation(
                            address: r.Locations!.Trim(),
                            funFact: string.IsNullOrWhiteSpace(r.FunFacts) ? null : r.FunFacts!.Trim(),
                            latitude: lat,
                            longitude: lng
                        );
                    })
                    // Evitar duplicados exactos de misma dirección/coords
                    .DistinctBy(l => (l.Address, l.Latitude, l.Longitude))
                    .ToList();

                return new Movie(
                    title: g.Key.Title,
                    releaseYear: g.Key.Year,
                    productionCompany: first.ProductionCompany,
                    director: first.Director,
                    writer: first.Writer,
                    cast: cast,
                    locations: locations
                );
            })
            .OrderBy(m => m.Title)
            .ThenBy(m => m.ReleaseYear)
            .ToList();
    }

    private static int? ParseInt(string? s) =>
        int.TryParse(s, out var v) ? v : (int?)null;
    private static double? TryParseDouble(string? s)
        => double.TryParse(s, System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out var d)
           ? d : (double?)null;
}