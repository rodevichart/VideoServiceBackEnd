using AutoMapper;
using Microsoft.Extensions.Logging;
using VideoServiceBL.DTOs;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Models;
using VideoServiceDAL.Persistence;

namespace VideoServiceBL.Services
{
    public class GenreService: BaseService<Genre, GenreDto>, IGenreService
    {


        public GenreService(VideoServiceDbContext context, ILogger<BaseService<Genre, GenreDto>> logger, IMapper mapper) : base(context, logger, mapper)
        {
        }
    }
}