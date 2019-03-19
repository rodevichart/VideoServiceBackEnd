using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace VideoServiceBL.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObject, Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (string.IsNullOrWhiteSpace(queryObject.OrderColumn) || !columnsMap.ContainsKey(queryObject.OrderColumn))
                return query;

            return queryObject.IsSortAscending
                ? query.OrderBy(columnsMap[queryObject.OrderColumn])
                : query.OrderByDescending(columnsMap[queryObject.OrderColumn]);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObject)
        {
            if (queryObject.PageIndex <= 0)
                queryObject.PageIndex = 10;

            if (queryObject.PageSize <= 0)
                queryObject.PageSize = 10;
            return query
                .Skip((queryObject.PageIndex - 1) * queryObject.PageSize)
                .Take(queryObject.PageSize);
        }
    }
}