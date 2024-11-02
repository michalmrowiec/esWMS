using esWMS.Domain.Entities.Documents;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations;
using FluentAssertions;

namespace esWMS.Infrastructure.UnitTests.Utilities.SieveProcessor.SortMethods.DocumentIssueDateSort
{
    public class DocumentIssueDateSortTests
    {
        [Theory]
        [MemberData(
            nameof(DocumentIssueDateTestsSortData.TestData),
            MemberType = typeof(DocumentIssueDateTestsSortData))]
        public void DocumentIssueDate_ForValidArgs_ReturnsSortedRecords(
            IQueryable<PZ> source,
            bool useThenBy,
            bool desc,
            IQueryable<PZ> expectedResult)
        {
            var sieveCustomFilter = new SieveCustomSortMethods();

            IQueryable<PZ> result;
            if (useThenBy)
            {
                result = sieveCustomFilter.DocumentIssueDate((IOrderedQueryable<PZ>)source, useThenBy, desc);
            }
            else
            {
                result = sieveCustomFilter.DocumentIssueDate(source, useThenBy, desc);
            }

            result.Should().BeEquivalentTo(
                expectedResult.AsQueryable(),
                options => options.WithStrictOrdering());
        }
    }
}
