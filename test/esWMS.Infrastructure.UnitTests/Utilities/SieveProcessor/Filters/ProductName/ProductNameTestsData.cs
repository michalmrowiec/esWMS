using esWMS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Infrastructure.UnitTests.Utilities.SieveProcessor.Filters.ProductName
{
    internal class ProductNameTestsData
    {
        public static IEnumerable<object[]> TestData =>
        [
            [
                new List<WarehouseUnit>()
                {
                    new()
                    {
                        WarehouseUnitId = "wu_1",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_11", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_12", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_13", Product = new() { ProductName = "prod_name_2" } }
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_2",
                        WarehouseUnitItems = [ ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_3",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_31", Product = new() { ProductName = "prod_name_2" } },
                            new() { WarehouseUnitId = "wui_32", Product = new() { ProductName = "prod_name_2" } },
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_4",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_41", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_42", Product = new() { ProductName = "prod_name_3" } },
                            new() { WarehouseUnitId = "wui_43", Product = new() { ProductName = "prod_name_4" } }
                        ]
                    }
                }.AsQueryable(),
                "@=",
                new string[] { "e_2" },
                new List<WarehouseUnit>()
                {
                    new()
                    {
                        WarehouseUnitId = "wu_1",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_11", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_12", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_13", Product = new() { ProductName = "prod_name_2" } }
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_3",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_31", Product = new() { ProductName = "prod_name_2" } },
                            new() { WarehouseUnitId = "wui_32", Product = new() { ProductName = "prod_name_2" } },
                        ]
                    }
                }.AsQueryable()
            ],
            [
                new List<WarehouseUnit>()
                {
                    new()
                    {
                        WarehouseUnitId = "wu_1",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_11", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_12", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_13", Product = new() { ProductName = "prod_name_2" } }
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_2",
                        WarehouseUnitItems = [ ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_3",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_31", Product = new() { ProductName = "prod_name_2" } },
                            new() { WarehouseUnitId = "wui_32", Product = new() { ProductName = "prod_name_2" } },
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_4",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_41", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_42", Product = new() { ProductName = "prod_name_3" } },
                            new() { WarehouseUnitId = "wui_43", Product = new() { ProductName = "prod_name_4" } }
                        ]
                    }
                }.AsQueryable(),
                "!@=",
                new string[] { "e_2" },
                new List<WarehouseUnit>()
                {
                    new()
                    {
                        WarehouseUnitId = "wu_1",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_11", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_12", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_13", Product = new() { ProductName = "prod_name_2" } }
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_4",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_41", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_42", Product = new() { ProductName = "prod_name_3" } },
                            new() { WarehouseUnitId = "wui_43", Product = new() { ProductName = "prod_name_4" } }
                        ]
                    }
                }.AsQueryable()
            ],
            [
                new List<WarehouseUnit>()
                {
                    new()
                    {
                        WarehouseUnitId = "wu_1",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_11", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_12", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_13", Product = new() { ProductName = "prod_name_2" } }
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_2",
                        WarehouseUnitItems = [ ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_3",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_31", Product = new() { ProductName = "prod_name_2" } },
                            new() { WarehouseUnitId = "wui_32", Product = new() { ProductName = "prod_name_2" } },
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_4",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_41", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_42", Product = new() { ProductName = "prod_name_3" } },
                            new() { WarehouseUnitId = "wui_43", Product = new() { ProductName = "prod_name_4" } }
                        ]
                    }
                }.AsQueryable(),
                "==",
                new string[] { "prod_name_3" },
                new List<WarehouseUnit>()
                {
                    new()
                    {
                        WarehouseUnitId = "wu_4",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_41", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_42", Product = new() { ProductName = "prod_name_3" } },
                            new() { WarehouseUnitId = "wui_43", Product = new() { ProductName = "prod_name_4" } }
                        ]
                    }
                }.AsQueryable()
            ],
            [
                new List<WarehouseUnit>()
                {
                    new()
                    {
                        WarehouseUnitId = "wu_1",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_11", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_12", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_13", Product = new() { ProductName = "prod_name_2" } }
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_2",
                        WarehouseUnitItems = [ ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_3",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_31", Product = new() { ProductName = "prod_name_2" } },
                            new() { WarehouseUnitId = "wui_32", Product = new() { ProductName = "prod_name_2" } },
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_4",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_41", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_42", Product = new() { ProductName = "prod_name_3" } },
                            new() { WarehouseUnitId = "wui_43", Product = new() { ProductName = "prod_name_4" } }
                        ]
                    }
                }.AsQueryable(),
                "!=",
                new string[] { "prod_name_3" },
                new List<WarehouseUnit>()
                {
                    new()
                    {
                        WarehouseUnitId = "wu_1",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_11", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_12", Product = new() { ProductName = "prod_name_1" } },
                            new() { WarehouseUnitId = "wui_13", Product = new() { ProductName = "prod_name_2" } }
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_2",
                        WarehouseUnitItems = [ ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_3",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_31", Product = new() { ProductName = "prod_name_2" } },
                            new() { WarehouseUnitId = "wui_32", Product = new() { ProductName = "prod_name_2" } },
                        ]
                    },
                }.AsQueryable()
            ]
        ];
    }
}
