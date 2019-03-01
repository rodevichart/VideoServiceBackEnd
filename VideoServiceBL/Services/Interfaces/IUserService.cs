using System.Threading.Tasks;
using VideoServiceBL.DTOs.UsersDtos;

namespace VideoServiceBL.Services.Interfaces
{
    public interface IUserService : IBaseService<UserDto>
    {
        Task<AuthenticateUserDto> AuthenticateAsync(string username, string password);
        Task<UserDto> CreateUserAsync(string userName, string password);
    }

}