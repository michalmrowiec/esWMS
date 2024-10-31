using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations;
using FluentAssertions;

namespace esWMS.Infrastructure.UnitTests.Utilities.SieveProcessor.Filters.AnyBlockedItem
{
    public class AnyBlockedItemTests
    {
        [Theory]
        [MemberData(
            nameof(AnyBlockedItemTestsData.TestData),
            MemberType = typeof(AnyBlockedItemTestsData))]
        public void AnyBlockedItem_ForValidArgs_RetursFilteredRecords(
            IQueryable<WarehouseUnit> source,
            string op,
            string[] values,
            IQueryable<WarehouseUnit> expectedResult)
        {
            var sieveCustomFilter = new SieveCustomFilterMethods();

            var result = sieveCustomFilter.AnyBlockedItem(source, op, values);

            result.Should().BeEquivalentTo(expectedResult.AsQueryable());
        }
    }
}
