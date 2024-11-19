using esWMS.Domain.Entities.Documents;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.Documents
{
    internal class WzSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<WZ>(x => x.DocumentId)
                .CanSort()
                .CanFilter();
            mapper.Property<WZ>(x => x.IssueWarehouseId)
                .CanSort()
                .CanFilter();
            mapper.Property<WZ>(x => x.IsApproved)
                .CanSort()
                .CanFilter();
            mapper.Property<WZ>(x => x.RecipientContractorId)
                .CanSort()
                .CanFilter();
        }
    }
}
