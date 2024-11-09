using esWMS.Application.Functions.Categories;
using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Domain.Models;
using Sieve.Models;

namespace esWMS.API.IntegrationTests.Controllers.WarehouseEnvironment.TestData
{
    internal class GetSortedAndFilteredCategoriesTestsData
    {
        public static IEnumerable<object[]> ValidaData =>
        [
            [
                new Category[]
                {
                    new() { CategoryId = "Cat_1", CategoryName = "tools" },
                    new() { CategoryId = "Cat_2", CategoryName = "electric" },
                    new() { CategoryId = "Cat_3", CategoryName = "materials" },
                    new() { CategoryId = "Cat_4", CategoryName = "test1" },
                    new() { CategoryId = "Cat_5", CategoryName = "Oil" }
                },
                new SieveModel()
                {
                    Filters = "",
                    Sorts = "CategoryName",
                    Page = 2,
                    PageSize = 2
                },
                new PagedResult<CategoryDto>()
                {
                    TotalPages = 3,
                    TotalItems = 5,
                    ItemsFrom = 3,
                    ItemsTo = 4,
                    Items =
                    [
                        new() { CategoryId = "Cat_5", CategoryName = "Oil" },
                        new() { CategoryId = "Cat_4", CategoryName = "test1" }
                    ]
                }
            ],
            [
                new Category[]
                {
                    new() { CategoryId = "Cat_1", CategoryName = "tools" },
                    new() { CategoryId = "Cat_2", CategoryName = "electric" },
                    new() { CategoryId = "Cat_3", CategoryName = "materials" },
                    new() { CategoryId = "Cat_4", CategoryName = "test1" },
                    new() { CategoryId = "Cat_5", CategoryName = "Oil" }
                },
                new SieveModel()
                {
                    Filters = "CategoryName@=rials",
                    Sorts = "",
                    Page = 1,
                    PageSize = 10
                },
                new PagedResult<CategoryDto>()
                {
                    TotalPages = 1,
                    TotalItems = 1,
                    ItemsFrom = 1,
                    ItemsTo = 10,
                    Items =
                    [
                        new() { CategoryId = "Cat_3", CategoryName = "materials" }
                    ]
                }
            ]

        ];
    }
}
