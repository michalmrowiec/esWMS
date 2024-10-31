using esWMS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Infrastructure.UnitTests.Utilities.SieveProcessor.Filters.AnyBlockedItem
{
    internal class AnyBlockedItemTestsData
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
                            new() { WarehouseUnitId = "wui_11", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_12", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_13", BlockedQuantity = 0 }
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
                            new() { WarehouseUnitId = "wui_31", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_32", BlockedQuantity = 1 }
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_4",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_41", BlockedQuantity = 1 },
                            new() { WarehouseUnitId = "wui_42", BlockedQuantity = 9 },
                            new() { WarehouseUnitId = "wui_43", BlockedQuantity = 2 },
                            new() { WarehouseUnitId = "wui_44", BlockedQuantity = 100 }
                        ]
                    }
                }.AsQueryable(),
                "==",
                new string[] { "true" },
                new List<WarehouseUnit>()
                {
                    new()
                    {
                        WarehouseUnitId = "wu_3",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_31", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_32", BlockedQuantity = 1 }
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_4",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_41", BlockedQuantity = 1 },
                            new() { WarehouseUnitId = "wui_42", BlockedQuantity = 9 },
                            new() { WarehouseUnitId = "wui_43", BlockedQuantity = 2 },
                            new() { WarehouseUnitId = "wui_44", BlockedQuantity = 100 }
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
                            new() { WarehouseUnitId = "wui_11", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_12", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_13", BlockedQuantity = 0 }
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
                            new() { WarehouseUnitId = "wui_31", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_32", BlockedQuantity = 1 }
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_4",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_41", BlockedQuantity = 1 },
                            new() { WarehouseUnitId = "wui_42", BlockedQuantity = 9 },
                            new() { WarehouseUnitId = "wui_43", BlockedQuantity = 2 },
                            new() { WarehouseUnitId = "wui_44", BlockedQuantity = 100 }
                        ]
                    }
                }.AsQueryable(),
                "==",
                new string[] { "false" },
                new List<WarehouseUnit>()
                {
                    new()
                    {
                        WarehouseUnitId = "wu_1",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_11", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_12", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_13", BlockedQuantity = 0 }
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_2",
                        WarehouseUnitItems = [ ]
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
                            new() { WarehouseUnitId = "wui_11", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_12", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_13", BlockedQuantity = 0 }
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
                            new() { WarehouseUnitId = "wui_31", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_32", BlockedQuantity = 1 }
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_4",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_41", BlockedQuantity = 1 },
                            new() { WarehouseUnitId = "wui_42", BlockedQuantity = 9 },
                            new() { WarehouseUnitId = "wui_43", BlockedQuantity = 2 },
                            new() { WarehouseUnitId = "wui_44", BlockedQuantity = 100 }
                        ]
                    }
                }.AsQueryable(),
                "!=",
                new string[] { "true" },
                new List<WarehouseUnit>()
                {
                    new()
                    {
                        WarehouseUnitId = "wu_1",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_11", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_12", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_13", BlockedQuantity = 0 }
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_2",
                        WarehouseUnitItems = [ ]
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
                            new() { WarehouseUnitId = "wui_11", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_12", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_13", BlockedQuantity = 0 }
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
                            new() { WarehouseUnitId = "wui_31", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_32", BlockedQuantity = 1 }
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_4",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_41", BlockedQuantity = 1 },
                            new() { WarehouseUnitId = "wui_42", BlockedQuantity = 9 },
                            new() { WarehouseUnitId = "wui_43", BlockedQuantity = 2 },
                            new() { WarehouseUnitId = "wui_44", BlockedQuantity = 100 }
                        ]
                    }
                }.AsQueryable(),
                "!=",
                new string[] { "false" },
                new List<WarehouseUnit>()
                {
                    new()
                    {
                        WarehouseUnitId = "wu_3",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_31", BlockedQuantity = 0 },
                            new() { WarehouseUnitId = "wui_32", BlockedQuantity = 1 }
                        ]
                    },
                    new()
                    {
                        WarehouseUnitId = "wu_4",
                        WarehouseUnitItems =
                        [
                            new() { WarehouseUnitId = "wui_41", BlockedQuantity = 1 },
                            new() { WarehouseUnitId = "wui_42", BlockedQuantity = 9 },
                            new() { WarehouseUnitId = "wui_43", BlockedQuantity = 2 },
                            new() { WarehouseUnitId = "wui_44", BlockedQuantity = 100 }
                        ]
                    }
                }.AsQueryable()
            ]
        ];
    }
}
