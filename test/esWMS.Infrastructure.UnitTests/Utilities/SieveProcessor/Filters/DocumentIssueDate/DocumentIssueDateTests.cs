using esWMS.Domain.Entities.Documents;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations;
using FluentAssertions;

namespace esWMS.Infrastructure.UnitTests.Utilities.SieveProcessor.Filters.DocumentIssueDate
{
    public class DocumentIssueDateTests
    {
        [Theory]
        [MemberData(
            nameof(DocumentIssueDateTestsData.TestData),
            MemberType = typeof(DocumentIssueDateTestsData))]
        public void DocumentIssueDate_ForValidArgs_RetursFilteredRecords(
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
