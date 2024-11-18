namespace esWMS.Application.Functions.Categories.Commands
{
    public class CommonCategoryCommand
    {
        public string CategoryName { get; set; } = null!;
        public string? ParentCategoryId { get; set; }
    }
}
