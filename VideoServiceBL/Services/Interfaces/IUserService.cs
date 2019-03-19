using System.Threading.Tasks;
using VideoServiceDAL.Models;

namespace VideoServiceBL.Services.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task<string> CreateUserAsync(string userName, string name, string password);

    }

}