using esWMS.Domain.Entities.Documents;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.Documents
{
    internal class PzSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<PZ>(x => x.DocumentId)
                .CanSort()
                .CanFilter();
            mapper.Property<PZ>(x => x.IssueWarehouseId)
                .CanSort()
                .CanFilter();
            mapper.Property<PZ>(x => x.IsApproved)
                .CanSort()
                .CanFilter();
            mapper.Property<PZ>(x => x.SupplierContractorId)
                .CanSort()
                .CanFilter();
        }
    }
}
