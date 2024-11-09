using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Domain.Services;
using FluentAssertions;

namespace esWMS.Domain.UnitTests.Services.LocationService
{
    public class LocationServiceTests
    {
        [Theory]
        [MemberData(
            nameof(LocationServiceTestsData.ValidTestDataForGenerateLocationId),
            MemberType = typeof(LocationServiceTestsData))]
        public void GenerateLocationId_ForValidData_ReturnsValidId(
            Location location,
            string result)
        {
            string locationId = location.GenerateLocationId();
            locationId.Should().Be(result);
        }
    }
}
