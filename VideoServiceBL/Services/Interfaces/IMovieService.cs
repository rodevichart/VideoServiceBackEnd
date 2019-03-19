using System.Collections.Generic;
using System.Threading.Tasks;
using VideoServiceBL.DTOs.MoviesDtos;
using VideoServiceDAL.Models;

namespace VideoServiceBL.Services.Interfaces
{
    public interface IMovieService : IBaseService<MovieDto>
    {
        Task UpdateMovieAsync(int id, MovieDataDto movieData);
        Task<MovieDataDto> AddMovieAsync(MovieDataDto movieData);

        Task<IEnumerable<Movie>> GetMovieWithGenreAsync();

        Task<QueryResultDto<MovieDto>> GetMovieWithGenreForDataTable(MovieDataTableSettings settings);

        Task<MovieDto> GetMovieWithGenreByIdAsync(long id);
    }
}
