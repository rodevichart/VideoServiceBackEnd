using System.Threading.Tasks;
using VideoServiceBL.DTOs.UsersDtos;
using VideoServiceDAL.Models;

namespace VideoServiceBL.Services.Interfaces
{
    public interface IUserService : IBaseService<UserDto>
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task<string> CreateUserAsync(string userName, string name, string password);

    }

}