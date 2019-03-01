using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoServiceBL.Services.Interfaces
{
    public interface IBaseService<TDto> where TDto : class
    {
        Task<TDto> GetAsync(int id);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task AddAsync(TDto dtoObj);
        Task AddRangeAsync(IList<TDto> dtoObjs);
        Task RemoveAsync(int id);
        Task RemoveRangeAsync(IList<TDto> dtoObjs);
        Task UpdateAsync(TDto dtoObj);
    }
}
