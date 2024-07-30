namespace esWMS.Client.ViewModels
{
    public class CategoryVM
    {
        public string CategoryId { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string? ParentCategoryId { get; set; }

        public CategoryVM? ParentCategory { get; set; }
    }
}