using System.Collections.Generic;
using System.Threading.Tasks;

namespace VideoServiceBL.Services.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<T> AddAsync(T obj);
        Task AddRangeAsync(IList<T> objs);
        Task<T> GetAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task RemoveAsync(long id);
        Task RemoveRangeAsync(IList<T> objs);
        Task UpdateAsync(T obj);
    }
}
