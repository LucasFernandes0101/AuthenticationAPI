namespace AuthenticationAPI.Domain.Base
{
    public class PagedResult<T>
    {
        public List<T>? Items { get; set; }
        public int Total { get; set; }

        public PagedResult(int total, List<T>? items)
        {
            Items = items;
            Total = total;
        }
    }
}