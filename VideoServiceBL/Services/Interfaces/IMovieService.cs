using System.Collections.Generic;
using System.Threading.Tasks;
using VideoServiceBL.DTOs.MoviesDtos;
using VideoServiceDAL.Models;

namespace VideoServiceBL.Services.Interfaces
{
    public interface IMovieService : IBaseService<Movie>
    {
        Task<IEnumerable<Movie>> GetMovieWithGenreAsync();

        Task<QueryResult<Movie>> GetMovieWithGenreForDataTable(MovieDataTableSettings settings);

        Task<Movie> GetMovieWithGenreByIdAsync(long id);
    }
}
