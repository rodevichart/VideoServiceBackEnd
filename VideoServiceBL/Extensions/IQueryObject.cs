namespace VideoServiceBL.Extensions
{
    public interface IQueryObject
    {
        string Search { get; set; }
        string OrderColumn { get; set; }
        bool IsSortAscending { get; set; }
        int PageIndex { get; set; }
        int PageSize { get; set; }
    }
}