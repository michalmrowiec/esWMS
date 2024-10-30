using esWMS.Application.Functions.Categories.Commands.CreateCategory;

namespace esWMS.API.IntegrationTests.Controllers.CategoryController.CreateCategory
{
    internal class CreateCategoryTestsData
    {
        public static IEnumerable<object[]> ValidaData =>
        [
            [new CreateCategoryCommand { CategoryName = "test" }],
            [new CreateCategoryCommand { CategoryName = "twenty five character 123" }],
            [new CreateCategoryCommand { CategoryName = "!@#$%^&*()_+TEST" }],
            [new CreateCategoryCommand { CategoryName = "TE ST" }],
            [new CreateCategoryCommand { CategoryName = "90" }]
        ];
    }
}
