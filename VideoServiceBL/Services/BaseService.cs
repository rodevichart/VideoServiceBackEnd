using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VideoServiceBL.Exceptions;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Persistence;

namespace VideoServiceBL.Services
{
    public abstract class BaseService<T, TDto> : IBaseService<TDto>
        where T : class
        where TDto : class
    {
        protected readonly DbSet<T> Entities;
        protected readonly VideoServiceDbContext Context;
        private readonly ILogger<BaseService<T, TDto>> _logger;
        private readonly IMapper _mapper;

        protected BaseService(VideoServiceDbContext context, ILogger<BaseService<T,TDto>> logger, IMapper mapper)
        {
            Context = context;
            _logger = logger;
            _mapper = mapper;
            Entities = context.Set<T>();
        }

        public async Task<TDto> AddAsync(TDto dtoObj)
        {
            try
            {
                var item = _mapper.Map<TDto, T>(dtoObj);
                await Entities.AddAsync(item);
                await Context.SaveChangesAsync();
                return _mapper.Map<T, TDto> (item);
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBase error, could`t add data", ex);
                throw new BusinessLogicException("Could not add data!", ex);
            }
        }

        public async Task AddRangeAsync(IList<TDto> dtoObjs)
        {
            try
            {
                var items = _mapper.Map<IList<TDto>, IList<T>>(dtoObjs);
                await Entities.AddRangeAsync(items);
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBase error, could`t add data", ex);
                throw new BusinessLogicException("Could not add data!", ex);
            }
        }

        public async Task<TDto> GetAsync(long id)
        {
            try
            {
                return  _mapper.Map<T,TDto>(await Entities.FindAsync(id)) ?? throw new BusinessLogicException("Item not found!");
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBase error, could`t get data by id", ex);
                throw new BusinessLogicException(ex.Message ?? "Could not get data!", ex);
            }
        }

        public async Task<IList<TDto>> GetAllAsync()
        {
            try
            {
                var list = await Entities.ToListAsync();
                return list.Select(_mapper.Map<T, TDto>).ToList();
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

        public async Task RemoveRangeAsync(IList<TDto> dtoObjs)
        {
            try
            {
                var items = _mapper.Map<IList<TDto>, IList<T>>(dtoObjs);
                Entities.RemoveRange(items);
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBase error, could`t remove data", ex);
                throw new BusinessLogicException("Could not remove data!", ex);
            }
        }

        public async Task UpdateAsync(TDto dtoObj)
        {
            try
            {
                var item = _mapper.Map<TDto, T>(dtoObj);
                Context.Entry(item).State = EntityState.Modified;
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
