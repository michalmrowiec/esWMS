namespace esMWS.Domain.Models
{
    public class PagedResult<T>
    {
        public IList<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalItems { get; set; }

        public PagedResult(IList<T> items, int totalCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItems = totalCount;
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        public PagedResult()
        {
            
        }
    }
}