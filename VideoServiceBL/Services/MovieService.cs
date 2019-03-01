using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VideoService.VideoServiceBL.Services;
using VideoServiceBL.DTOs.MoviesDtos;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Models;
using VideoServiceDAL.Persistence;

namespace VideoServiceBL.Services
{
    public class MovieService : BaseService<Movie, MovieDto>, IMovieService
    {
        private readonly IMapper _mapper;

        public MovieService(VideoServiceDbContext context, IMapper mapper) 
            : base(context, mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<ListOfMoviesDto>> GetMovieWithGenreAsync()
        {
            var list = await Entities.Include(m => m.Genre).ToListAsync();
            return list.Select(_mapper.Map<Movie, ListOfMoviesDto>);
        }
    }
}
