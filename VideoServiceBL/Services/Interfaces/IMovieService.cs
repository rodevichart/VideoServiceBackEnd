using System.Collections.Generic;
using System.Threading.Tasks;
using VideoServiceBL.DTOs.MoviesDtos;

namespace VideoServiceBL.Services.Interfaces
{
    public interface IMovieService : IBaseService<MovieDto>
    {
        Task<IEnumerable<ListOfMoviesDto>> GetMovieWithGenreAsync();
    }
}
