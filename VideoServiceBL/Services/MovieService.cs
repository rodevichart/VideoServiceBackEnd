using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VideoServiceBL.DTOs.MoviesDtos;
using VideoServiceBL.Exceptions;
using VideoServiceBL.Extensions;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Models;
using VideoServiceDAL.Persistence;

namespace VideoServiceBL.Services
{
    public class MovieService : BaseService<Movie>, IMovieService
    {
        private readonly ILogger<MovieService> _logger;

        public MovieService(VideoServiceDbContext context, ILogger<MovieService> logger)
            : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<Movie> GetMovieWithGenreByIdAsync(long id)
        {
            try
            {
                return await Entities.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBase error, could`t get movie by id", ex);
                throw new BusinessLogicException("Could not fetch data!", ex);
            }
        }

        public async Task<IEnumerable<Movie>> GetMovieWithGenreAsync()
        {
            return await Entities.Include(m => m.Genre).ToListAsync(); 
        }

        public async Task<QueryResult<Movie>> GetMovieWithGenreForDataTable(
            MovieDataTableSettings settings)
        {
            try
            {
                var query = Context.Movies.AsQueryable();
                var totalRecords = await query.CountAsync();

                if (!string.IsNullOrEmpty(settings.Search))
                {
                    query = SearchMovie(settings.Search, query);
                    totalRecords = await query.CountAsync();
                }

                var columnsMap = GetColumnsMap();


                if (settings.GenreId.HasValue)
                {
                    query = query.Where(m => m.GenreId == settings.GenreId);
                    totalRecords = await query.CountAsync();
                }

                query = query
                    .Include(m => m.Genre)
                    .ApplyOrdering(settings, columnsMap);

                query = query.ApplyPaging(settings);

                var result = await query.ToListAsync();

                return new QueryResult<Movie>
                {
                    Items = result,
                    TotalItems = totalRecords
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBase error", ex);
                throw new BusinessLogicException("Could not fetch data!", ex);
            }
        }

        private static Dictionary<string, Expression<Func<Movie, object>>> GetColumnsMap()
        {
            return new Dictionary<string, Expression<Func<Movie, object>>>
            {
                ["name"] = m => m.Name,
                ["genre.name"] = m => m.Genre.Name,
                ["numberInStock"] = m => m.NumberInStock,
                ["rate"] = m => m.Rate
            };
        }

        private IQueryable<Movie> SearchMovie(string searchString, IQueryable<Movie> query)
        {

             return query.Where(m => m.Name.ToLower().Contains(searchString.ToLower())
                                     ||
                                     m.Genre.Name.ToLower().Contains(searchString.ToLower())
            );
        }
    }
}
