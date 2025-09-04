using Microsoft.AspNetCore.Mvc;
using SFMovies.Application.DTOs;
using SFMovies.Application.Interfaces;

namespace SFMoviesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMovieService movieService;

        public MoviesController(ILogger<MoviesController> logger, IMovieService movieService)
        {
            _logger = logger;
            this.movieService = movieService;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieDto>> GetAll(string? title = null) => await movieService.GetAll(title);

        [HttpGet("title/suggest")]
        public Task<List<TittleSuggestionDto>> Suggest([FromQuery] string prefix, [FromQuery] int limit = 10, CancellationToken ct = default)
        => movieService.SuggestTitlesAsync(prefix, limit, ct);
    }
}
