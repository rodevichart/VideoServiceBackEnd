using Microsoft.Extensions.Logging;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Models;
using VideoServiceDAL.Persistence;

namespace VideoServiceBL.Services
{
    public class GenreService: BaseService<Genre>, IGenreService
    {


        public GenreService(VideoServiceDbContext context, ILogger<BaseService<Genre>> logger) : base(context, logger)
        {
        }
    }
}