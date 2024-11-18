namespace esWMS.Domain.Entities.WarehouseEnviroment
{
    public class Category
    {
        public string CategoryId { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string? ParentCategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public IList<Product> Products { get; set; } = [];
        public Category? ParentCategory { get; set; }
        public IList<Category> ChildCategories { get; set; } = [];
    }
}
