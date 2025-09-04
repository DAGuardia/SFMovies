using SFMovies.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMovies.Application.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAll(string? title);
        Task<List<TittleSuggestionDto>> SuggestTitlesAsync(string prefix, int limit = 10, CancellationToken ct = default);
    }
}
