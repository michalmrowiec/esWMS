namespace esWMS.Client.ViewModels
{
    public class PagedResultVM<T>
    {
        public IList<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalItems { get; set; }
    }
}
