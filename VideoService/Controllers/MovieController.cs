using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoServiceBL.DTOs.MoviesDtos;
using VideoServiceBL.Services.Interfaces;

namespace VideoService.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<QueryResultDto<MovieDto>> GetMoviesAsync([FromQuery] MovieDataTableSettings model)
        {
            return await
                _movieService.GetMovieWithGenreForDataTable(model);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<MovieDto> GetMovieByIdAsync(long id)
        {
            return await
                _movieService.GetMovieWithGenreWithCoverByIdAsync(id);
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<MovieDataDto> AddMovieAsync(MovieDataDto movieData)
        {
            return await _movieService.AddMovieAsync(movieData);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task UpdateMovieAsync(int id, MovieDataDto movieData)
        {
            await _movieService.UpdateMovieAsync(id, movieData);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveMovieAsync(long id)
        {
            await _movieService.RemoveAsync(id);
            return Ok();
        }

    }
}
