using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Persistence;

namespace VideoService.VideoServiceBL.Services
{
    public abstract class BaseService<T, TDto> : IBaseService<TDto>
        where T : class
        where TDto : class
    {
        protected readonly DbSet<T> Entities;
        protected readonly IMapper Mapper;
        protected readonly VideoServiceDbContext Context;

        protected BaseService(VideoServiceDbContext context, IMapper mapper)
        {
            Mapper = mapper;
            Context = context;
            Entities = context.Set<T>();
        }

        public async Task AddAsync(TDto dtoObj)
        {
            var item = Mapper.Map<TDto, T>(dtoObj);
            await Entities.AddAsync(item);
            await Context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IList<TDto> dtoObjs)
        {
            var items = Mapper.Map<IList<TDto>, IList<T>>(dtoObjs);
            await Entities.AddRangeAsync(items);
        }
        
        public async Task<TDto> GetAsync(int id)
        {
            var entity = await Entities.FindAsync(id);
            return Mapper.Map<T, TDto>(entity);
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var list = await Entities.ToListAsync();
            return list.Select(Mapper.Map<T, TDto>);
        }

        public async Task RemoveAsync(int id)
        {
            var item = await Entities.FindAsync(id);
            if (item != null)
            {
                Entities.Remove(item);
                await Context.SaveChangesAsync();
            }
        }

        public async Task RemoveRangeAsync(IList<TDto> dtoObjs)
        {
            var items = Mapper.Map<IList<TDto>, IList<T>>(dtoObjs);
            Entities.RemoveRange(items);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TDto dtoObj)
        {
            if (dtoObj != null)
            {
                var item = Mapper.Map<TDto, T>(dtoObj);
                Context.Entry(item).State = EntityState.Modified;
                await Context.SaveChangesAsync();
            }
        }

    }
}
