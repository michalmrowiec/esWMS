using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Domain.UnitTests.Services.LocationService
{
    internal class LocationServiceTestsData
    {
        public static IEnumerable<object[]> ValidTestDataForGenerateLocationId =>
        [
            [
                new Location
                {
                    ZoneId = "MTB/Z",
                    Row = 5,
                    Column = 'B',
                    Level = 2,
                    Cell = 1,
                    Capacity = 1000.0m,
                    MaxLength = 2.5,
                    MaxWidth = 1.5,
                    MaxHeight = 2.0,
                    MaxWeight = 500.0,
                    DefaultMediaTypeId = "MEDIA_TYPE_1",
                    CreatedAt = new DateTime(2022, 12, 01),
                    CreatedBy = "admin",
                },
                "MTB/Z/05/B/2/1"
            ],
            [
                new Location
                {
                    ZoneId = "!@#/1",
                    Row = 99,
                    Column = 'c',
                    Level = 0,
                    Cell = 9,
                    Capacity = 1000.0m,
                    MaxLength = 2.5,
                    MaxWidth = 1.5,
                    MaxHeight = 2.0,
                    MaxWeight = 500.0,
                    DefaultMediaTypeId = "MEDIA_TYPE_1",
                    CreatedAt = new DateTime(2022, 12, 01),
                    CreatedBy = "admin",
                },
                "!@#/1/99/C/0/9"
            ]
        ];

    }
}
