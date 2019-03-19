using VideoServiceBL.Extensions;

namespace VideoServiceBL.DTOs.MoviesDtos
{
    public class MovieDataTableSettings:IQueryObject
    {
        public int? GenreId { get; set; } 
        public string Search { get; set; }
        public string OrderColumn { get; set; }
        public bool IsSortAscending { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}