using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideoServiceBL.DTOs;
using VideoServiceBL.Services.Interfaces;

namespace VideoService.Controllers
{
    [ApiController]
    [Route("/api/genres")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IList<GenreDto>> GetGenresAsync()
        {
            return await _genreService.GetAllAsync();
        }
    }
}