using System.Collections.Generic;

namespace VideoServiceBL.DTOs.MoviesDtos
{
    public class QueryResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalItems { get; set; }
    }
}