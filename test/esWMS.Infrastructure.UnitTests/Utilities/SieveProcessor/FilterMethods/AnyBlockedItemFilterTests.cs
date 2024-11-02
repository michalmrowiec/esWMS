using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Infrastructure.UnitTests.Utilities.SieveProcessor.FilterMethods.TestDatas;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations;
using FluentAssertions;

namespace esWMS.Infrastructure.UnitTests.Utilities.SieveProcessor.FilterMethods
{
    public class AnyBlockedItemFilterTests
    {
        [Theory]
        [MemberData(
            nameof(AnyBlockedItemTestsFilterData.TestData),
            MemberType = typeof(AnyBlockedItemTestsFilterData))]
        public void AnyBlockedItem_ForValidArgs_ReturnsFilteredRecords(
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
