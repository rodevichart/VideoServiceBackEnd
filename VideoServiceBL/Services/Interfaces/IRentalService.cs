using System.Threading.Tasks;
using VideoServiceBL.DTOs.MoviesDtos;
using VideoServiceBL.DTOs.RentalsDtos;

namespace VideoServiceBL.Services.Interfaces
{
    public interface IRentalService : IBaseService<RentalDto>
    {
        Task<QueryResultDto<RentalDto>> GetAllRentalMoviesAsync(RentalDataTableSettings settings);

        Task<QueryResultDto<RentalDto>> GetAllRentalMoviesWithUsersAsync(RentalDataTableSettings settings);

        Task AddRentalByUserIdAndMovieIdAsync(AddRentalDto model);
    }
}