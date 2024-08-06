namespace esWMS.Client.ViewModels
{
    public class SieveModelVM
    {
        public SieveModelVM()
        {
            
        }
        public SieveModelVM(int page, int pageSize, string filters, string sorts)
        {
            Page = page;
            PageSize = pageSize;
            Filters = filters;
            Sorts = sorts;
        }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Filters { get; set; } = string.Empty;
        public string Sorts { get; set; } = string.Empty;
    }
}
