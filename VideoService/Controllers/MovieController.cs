using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideoServiceBL.DTOs.MoviesDtos;
using VideoServiceBL.Services.Interfaces;

namespace VideoService.Controllers
{
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet("/api/movies")]
        //[Authorize(Roles = "Admin")]
        public async Task<IEnumerable<ListOfMoviesDto>> GetMoviesAsync()
        {
            return await _movieService.GetMovieWithGenreAsync();
        }

    }
}
