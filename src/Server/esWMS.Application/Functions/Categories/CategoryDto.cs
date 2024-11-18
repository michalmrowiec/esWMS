using esWMS.Application.Functions.Products;

namespace esWMS.Application.Functions.Categories
{
    public class CategoryDto
    {
        public string CategoryId { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string? ParentCategoryId { get; set; }

        public CategoryDto? ParentCategory { get; set; }
    }
}
