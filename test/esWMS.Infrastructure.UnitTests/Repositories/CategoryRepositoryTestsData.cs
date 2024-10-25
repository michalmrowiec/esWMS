using esWMS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Infrastructure.UnitTests.Repositories
{
    internal static class CategoryRepositoryTestsData
    {
        public static IEnumerable<object[]> ValidData =>
        [
            [
                new Category()
                {
                    CategoryId = "fF46D4sfg5-sfg$3gfs",
                    CategoryName = "Cat_1",
                    CreatedAt = new DateTime(2023, 10, 23)
                }
            ]
        ];
    }
}