using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoServiceBL.Services.Interfaces
{
    public interface IBaseService<TDto> where TDto : class
    {
        Task<TDto> AddAsync(TDto dtoObj);
        Task AddRangeAsync(IList<TDto> dtoObjs);
        Task<TDto> GetAsync(long id);
        Task<IList<TDto>> GetAllAsync();
        Task RemoveAsync(long id);
        Task RemoveRangeAsync(IList<TDto> dtoObjs);
        Task UpdateAsync(TDto dtoObj, long? id = null);
    }
}
