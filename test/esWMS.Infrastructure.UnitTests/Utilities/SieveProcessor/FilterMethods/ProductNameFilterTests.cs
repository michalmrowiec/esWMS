using esWMS.Domain.Entities.WarehouseEnvironment;
using esWMS.Infrastructure.UnitTests.Utilities.SieveProcessor.FilterMethods.TestData;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations;
using FluentAssertions;

namespace esWMS.Infrastructure.UnitTests.Utilities.SieveProcessor.FilterMethods
{
    public class ProductNameFilterTests
    {
        [Theory]
        [MemberData(
            nameof(ProductNameTestsFilterData.TestData),
            MemberType = typeof(ProductNameTestsFilterData))]
        public void ProductName_ForValidArgs_ReturnsFilteredRecords(
            IQueryable<WarehouseUnit> source,
            string op,
            string[] values,
            IQueryable<WarehouseUnit> expectedResult)
        {
            var sieveCustomFilter = new SieveCustomFilterMethods();

            var result = sieveCustomFilter.ProductName(source, op, values);

            result.Should().BeEquivalentTo(expectedResult.AsQueryable());
        }
    }
}
