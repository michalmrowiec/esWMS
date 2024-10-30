using esWMS.Application.Functions.Categories;
using esWMS.Application.Functions.Categories.Commands.CreateCategory;
using esWMS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Application.UnitTests.Functions.Categories
{
    internal class CreateCategoryCommandHandlerTestsData
    {
        public static IEnumerable<object[]> ValidData => new List<object[]>
        {
            new object[]
            {
                new CreateCategoryCommand()
                {
                    CategoryName = "C1"
                },
                new Category()
                {
                    CategoryId = new Guid("00000001-0000-0000-0000-122000000000").ToString(),
                    CategoryName = "C1",
                    CreatedAt = new DateTime(2023, 10, 23)
                },
                new CategoryDto()
                {
                    CategoryId = new Guid("00000001-0000-0000-0000-122000000000").ToString(),
                    CategoryName = "C1",
                }
            }
        };
    }
}
