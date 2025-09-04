using SFMovies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMovies.Application.Services
{
    public interface IDataSFClient
    {
        Task<List<Movie>> GetMoviesAsync(string? title = null, int limit = 100, int offset = 0, CancellationToken ct = default);
        Task<List<string>> SuggestTitlesAsync(string prefix, int limit, CancellationToken ct);
    }
}
