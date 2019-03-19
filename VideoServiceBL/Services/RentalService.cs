using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VideoServiceBL.DTOs.MoviesDtos;
using VideoServiceBL.DTOs.RentalsDtos;
using VideoServiceBL.Exceptions;
using VideoServiceBL.Extensions;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Models;
using VideoServiceDAL.Persistence;

namespace VideoServiceBL.Services
{
    public class RentalService: BaseService<Rental>, IRentalService
    {
        private readonly ILogger<RentalService> _logger;

        public RentalService(VideoServiceDbContext context, ILogger<RentalService> logger)
            : base(context, logger)
        {
            _logger = logger;
        }

        public async Task<QueryResult<Rental>> GetAllRentalMoviesAsync(
            RentalDataTableSettings settings)
        {
            if (!settings.UserId.HasValue)
            {
                _logger.LogError("Id of user is required!");
                throw new BusinessLogicException("Something goes wrong try to Re-login!");
            }

            return await GetRentalMoviesAsync(settings);
        }

        public async Task<QueryResult<Rental>> GetAllRentalMoviesWithUsersAsync(
            RentalDataTableSettings settings)
        {
            return await GetRentalMoviesAsync(settings);
        }

        private async Task<QueryResult<Rental>> GetRentalMoviesAsync(
            RentalDataTableSettings settings)
        {
            try
            {
                var query = Context.Rentals.AsQueryable();

                if (settings.UserId > 0)
                {
                    query = query.Where(m => m.User.Id == (settings.UserId));
                }
                var totalRecords = await query.CountAsync();


                if (!string.IsNullOrEmpty(settings.Search))
                {

                    query = SearchRentals(settings.Search, query);
                    totalRecords = query.Count();
                }

                var columnsMap = GetColumnsMap();

                query = query
                    .Include(r => r.Movie)
                        .ThenInclude(m => m.Genre)
                    .Include(r => r.User)
                    .ApplyOrdering(settings, columnsMap);

//                if (settings.UserId <= 0)
//                {
//                    query = query.Include(r => r.User);
//                }

                query = query.ApplyPaging(settings);

                var result = await query.ToListAsync();

                return new QueryResult<Rental>
                {
                    Items = result,
                    TotalItems = totalRecords
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("DataBase error, could`t fetch rentals", ex);
                throw new BusinessLogicException("Could not fetch data!", ex);
            }
        }

        private static Dictionary<string, Expression<Func<Rental, object>>> GetColumnsMap()
        {
            return new Dictionary<string, Expression<Func<Rental, object>>>
            {
                ["movie.name"] = r => r.Movie.Name,
                ["user.userName"] = r => r.User.Username,
                ["dateRented"] = r => r.DateRented,
                ["dateReturned"] = r => r.DateReturned
            };
        }

        private IQueryable<Rental> SearchRentals(string searchString, IQueryable<Rental> query)
        {

            return query.Where(r => r.User.Name.ToLower().Contains(searchString.ToLower())
                                     ||
                                     r.Movie.Name.ToLower().Contains(searchString.ToLower())
                                     ||
                                     r.DateRented.ToString(CultureInfo.CurrentCulture).ToLower().Contains(searchString.ToLower())
                                     ||
                                     r.DateReturned.Value.ToString(CultureInfo.CurrentCulture).ToLower().Contains(searchString.ToLower())
            );
        }
    }
}