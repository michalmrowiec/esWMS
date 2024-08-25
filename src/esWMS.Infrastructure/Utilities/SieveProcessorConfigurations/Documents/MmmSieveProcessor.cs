using esMWS.Domain.Entities.Documents;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.Documents
{
    internal class MmmSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<MMM>(x => x.DocumentId)
                .CanSort()
                .CanFilter();
            mapper.Property<MMM>(x => x.IssueWarehouseId)
                .CanSort()
                .CanFilter();
            mapper.Property<MMM>(x => x.IsApproved)
                .CanSort()
                .CanFilter();
            mapper.Property<MMM>(x => x.IssueWarehouseId)
                .CanSort()
                .CanFilter();
        }
    }
}
