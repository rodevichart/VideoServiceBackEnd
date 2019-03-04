using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideoServiceBL.DTOs;
using VideoServiceBL.Services.Interfaces;

namespace VideoService.Controllers
{
    [ApiController]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet("/api/genres")]
        public async Task<IEnumerable<GenreDto>> GetMoviesAsync()
        {
            return await _genreService.GetAllAsync();
        }
    }
}