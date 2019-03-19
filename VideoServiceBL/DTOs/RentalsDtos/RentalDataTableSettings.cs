using VideoServiceBL.Extensions;

namespace VideoServiceBL.DTOs.RentalsDtos
{
    public class RentalDataTableSettings: IQueryObject
    {
        public long? UserId { get; set; }
        public string Search { get; set; }
        public string OrderColumn { get; set; }
        public bool IsSortAscending { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}