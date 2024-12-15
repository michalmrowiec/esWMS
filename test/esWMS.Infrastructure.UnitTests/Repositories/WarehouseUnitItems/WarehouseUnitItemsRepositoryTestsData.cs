using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Infrastructure.UnitTests.Repositories.WarehouseUnitItems
{
    internal class WarehouseUnitItemsRepositoryTestsData
    {
        public static IEnumerable<object[]> ValidData =>
        [
            [
                new Category[]
                {
                    new()
                    {
                        CategoryId = "C1",
                        CategoryName = "Cat_1",
                        CreatedAt = new DateTime(2023, 10, 23)
                    }
                },
                new Product[]
                {
                    new()
                    {
                        ProductId = "P1",
                        CategoryId = "C1",
                        ProductCode = "1",
                        ProductName = "P1_name",
                        ShortProductName = "P1_sn",
                        IsWeight = false,
                        IsMedia = false,
                        IsActive = true
                    }
                },
                new Warehouse()
                {
                    WarehouseId = "MPT",
                    WarehouseName = "Magazyn przyjęcia towaru",
                    WarehouseUnits =
                    [
                        new()
                        {
                            WarehouseUnitId = "WU_ID_1",
                            WarehouseUnitItems =
                            [
                                new()
                                {
                                    WarehouseUnitItemId = "WUI_ID_1",
                                    ProductId = "P1",
                                    Quantity = 10.0m,
                                    BlockedQuantity = 0.0m,
                                }
                            ]
                        }
                    ]
                },
                new Dictionary<string, decimal>()
                {
                    { "WUI_ID_1", 5.0m }
                },
                new List<WarehouseUnitItem>()
                {
                    new()
                    {
                        WarehouseUnitItemId = "WUI_ID_1",
                        WarehouseUnitId = "WU_ID_1",
                        ProductId = "P1",
                        Quantity = 10.0m,
                        BlockedQuantity = 5.0m,
                    }
                }
            ]
        ];
    }
}
