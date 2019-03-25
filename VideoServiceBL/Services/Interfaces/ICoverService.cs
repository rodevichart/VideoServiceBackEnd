using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using VideoServiceBL.DTOs;

namespace VideoServiceBL.Services.Interfaces
{
    public interface ICoverService:IBaseService<CoverDto>
    {
        Task<CoverDto> SaveImage(long movieId, IFormFile file);
    }
}