using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VideoServiceBL.Exceptions;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Persistence;

namespace VideoServiceBL.Services
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly DbSet<T> Entities;
        protected readonly VideoServiceDbContext Context;
        private readonly ILogger<BaseService<T>> _logger;

        protected BaseService(VideoServiceDbContext context, ILogger<BaseService<T>> logger)
        {
            Context = context;
            _logger = logger;
            Entities = context.Set<T>();
        }

        public async Task<T> AddAsync(T obj)
        {
            try
            {
                await Entities.AddAsync(obj);
                await Context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBase error, could`t add data", ex);
                throw new BusinessLogicException("Could not add data!", ex);
            }
        }

        public async Task AddRangeAsync(IList<T> objs)
        {
            try
            {
                await Entities.AddRangeAsync(objs);
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBase error, could`t add data", ex);
                throw new BusinessLogicException("Could not add data!", ex);
            }
        }

        public async Task<T> GetAsync(long id)
        {
            try
            {
                return await Entities.FindAsync(id) ?? throw new BusinessLogicException("Item not found!");
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBase error, could`t get data by id", ex);
                throw new BusinessLogicException(ex.Message ?? "Could not get data!", ex);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await Entities.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBase error, could`t get data", ex);
                throw new BusinessLogicException("Could not get data!", ex);
            }
        }

        public async Task RemoveAsync(long id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new BusinessLogicException(@"Invalid remove id - {id}!");
                }

                var item = await Entities.FindAsync(id);
                if (item != null)
                {
                    Entities.Remove(item);
                    await Context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBase error, could`t remove data", ex);
                throw new BusinessLogicException("Could not remove data!", ex);
            }
        }

        public async Task RemoveRangeAsync(IList<T> objs)
        {
            try
            {
                Entities.RemoveRange(objs);
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBase error, could`t remove data", ex);
                throw new BusinessLogicException("Could not remove data!", ex);
            }
        }

        public async Task UpdateAsync(T obj)
        {
            try
            {
                Context.Entry(obj).State = EntityState.Modified;
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBase error, could`t update data", ex);
                throw new BusinessLogicException("Could not update data!", ex);
            }

        }

    }
}
