using esWMS.Domain.Entities.Documents;
using esWMS.Infrastructure.UnitTests.Utilities.SieveProcessor.FilterMethods.TestDatas;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations;
using FluentAssertions;

namespace esWMS.Infrastructure.UnitTests.Utilities.SieveProcessor.FilterMethods
{
    public class DocumentIssueDateFilterTests
    {
        [Theory]
        [MemberData(
            nameof(DocumentIssueDateTestsFilterData.TestData),
            MemberType = typeof(DocumentIssueDateTestsFilterData))]
        public void DocumentIssueDate_ForValidArgs_ReturnsFilteredRecords(
            IQueryable<PZ> source,
            string op,
            string[] values,
            IQueryable<PZ> expectedResult)
        {
            var sieveCustomFilter = new SieveCustomFilterMethods();

            var result = sieveCustomFilter.DocumentIssueDate(source, op, values);

            result.Should().BeEquivalentTo(expectedResult.AsQueryable());
        }
    }
}
