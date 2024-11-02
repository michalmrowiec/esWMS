using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations;
using FluentAssertions;

namespace esWMS.Infrastructure.UnitTests.Utilities.SieveProcessor.FilterMethods.ProductNameFilter
{
    public class ProductNameFilterTests
    {
        [Theory]
        [MemberData(
            nameof(ProductNameTestsFilterData.TestData),
            MemberType = typeof(ProductNameTestsFilterData))]
        public void ProductName_ForValidArgs_RetursFilteredRecords(
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
