using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VideoServiceBL.DTOs;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Models;

namespace VideoService.Controllers
{
    [ApiController]
    [Route("/api/genres")]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreDto>> GetGenresAsync()
        {
            var genres = await _genreService.GetAllAsync();
            return genres.Select(_mapper.Map<Genre, GenreDto>);
        }
    }
}