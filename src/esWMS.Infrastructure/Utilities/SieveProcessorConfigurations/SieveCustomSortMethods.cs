using esWMS.Domain.Entities.Documents;
using esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.Documents;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations
{
    public class SieveCustomSortMethods : ISieveCustomSortMethods
    {
        public IQueryable<PZ> DocumentIssueDate(IQueryable<PZ> source, bool useThenBy, bool desc) =>
            BaseDocumentSieveProcessor.DocumentIssueDateSort(source, useThenBy, desc);
        public IQueryable<WZ> DocumentIssueDate(IQueryable<WZ> source, bool useThenBy, bool desc) =>
            BaseDocumentSieveProcessor.DocumentIssueDateSort(source, useThenBy, desc);
        public IQueryable<MMM> DocumentIssueDate(IQueryable<MMM> source, bool useThenBy, bool desc) =>
            BaseDocumentSieveProcessor.DocumentIssueDateSort(source, useThenBy, desc);
        public IQueryable<MMP> DocumentIssueDate(IQueryable<MMP> source, bool useThenBy, bool desc) =>
            BaseDocumentSieveProcessor.DocumentIssueDateSort(source, useThenBy, desc);
        public IQueryable<PW> DocumentIssueDate(IQueryable<PW> source, bool useThenBy, bool desc) =>
            BaseDocumentSieveProcessor.DocumentIssueDateSort(source, useThenBy, desc);
        public IQueryable<RW> DocumentIssueDate(IQueryable<RW> source, bool useThenBy, bool desc) =>
            BaseDocumentSieveProcessor.DocumentIssueDateSort(source, useThenBy, desc);
        public IQueryable<ZW> DocumentIssueDate(IQueryable<ZW> source, bool useThenBy, bool desc) =>
            BaseDocumentSieveProcessor.DocumentIssueDateSort(source, useThenBy, desc);
    }
}
