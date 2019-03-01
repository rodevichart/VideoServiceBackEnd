using AutoMapper;
using VideoService.VideoServiceBL.Services;
using VideoServiceBL.DTOs;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Models;
using VideoServiceDAL.Persistence;

namespace VideoServiceBL.Services
{
    public class GenreService: BaseService<Genre, GenreDto>, IGenreService
    {
        public GenreService(VideoServiceDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}