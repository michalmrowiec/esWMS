using esMWS.Domain.Entities.Documents;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.Documents
{
    internal class MmpSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<MMP>(x => x.DocumentId)
                .CanSort()
                .CanFilter();
            mapper.Property<MMP>(x => x.IssueWarehouseId)
                .CanSort()
                .CanFilter();
            mapper.Property<MMP>(x => x.IsApproved)
                .CanSort()
                .CanFilter();
            mapper.Property<MMP>(x => x.FromWarehouseId)
                .CanSort()
                .CanFilter();
            mapper.Property<MMP>(x => x.RelatedMmmId)
                .CanSort()
                .CanFilter();
        }
    }
}
