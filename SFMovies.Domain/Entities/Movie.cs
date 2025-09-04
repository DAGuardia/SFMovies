using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMovies.Domain.Entities
{
    public class Movie
    {
        public string Title { get; }
        public int? ReleaseYear { get; }
        public string? ProductionCompany { get; }
        public string? Director { get; }
        public string? Writer { get; }
        public IReadOnlyList<string> Cast { get; }
        public IReadOnlyList<MovieLocation> Locations { get; }

        public Movie(
            string title,
            int? releaseYear = null,
            string? productionCompany = null,
            string? director = null,
            string? writer = null,
            IReadOnlyList<string>? cast = null,
            IReadOnlyList<MovieLocation>? locations = null)
        {
            Title = title;
            ReleaseYear = releaseYear;
            ProductionCompany = productionCompany;
            Director = director;
            Writer = writer;
            Cast = cast ?? Array.Empty<string>();
            Locations = locations ?? Array.Empty<MovieLocation>();
        }
    }
}
