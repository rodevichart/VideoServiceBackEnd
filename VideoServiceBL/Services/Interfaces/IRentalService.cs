using System.Threading.Tasks;
using VideoServiceBL.DTOs.MoviesDtos;
using VideoServiceBL.DTOs.RentalsDtos;
using VideoServiceDAL.Models;

namespace VideoServiceBL.Services.Interfaces
{
    public interface IRentalService : IBaseService<Rental>
    {
        Task<QueryResult<Rental>> GetAllRentalMoviesAsync(RentalDataTableSettings settings);

        Task<QueryResult<Rental>> GetAllRentalMoviesWithUsersAsync(RentalDataTableSettings settings);
    }
}