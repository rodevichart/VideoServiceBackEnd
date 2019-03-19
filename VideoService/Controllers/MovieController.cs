using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoServiceBL.DTOs.MoviesDtos;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Models;

namespace VideoService.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<QueryResultDto<MovieDto>> GetMoviesAsync([FromQuery] MovieDataTableSettings model)
        {
            var queryResult = await
                _movieService.GetMovieWithGenreForDataTable(model);
            return _mapper.Map<QueryResult<Movie>, QueryResultDto<MovieDto>>(queryResult);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<MovieDto> GetMovieByIdAsync(long id)
        {
            var movieList = await
                _movieService.GetMovieWithGenreByIdAsync(id);
            return _mapper.Map<Movie, MovieDto>(movieList);
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<MovieDataDto> AddMovieAsync(MovieDataDto movieData)
        {
            var movieDto = _mapper.Map<MovieDataDto, Movie>(movieData);
            var addingItem = await _movieService.AddAsync(movieDto);
            return _mapper.Map<Movie, MovieDataDto>(addingItem);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task UpdateMovie(int id, MovieDataDto movieData)
        {
            var movieDto = _mapper.Map<MovieDataDto, Movie>(movieData);
            movieDto.Id = id;
            await _movieService.UpdateAsync(movieDto);
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
