using esWMS.Domain.Entities.Documents;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.Documents
{
    internal class ZwSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<ZW>(x => x.DocumentId)
                .CanSort()
                .CanFilter();
            mapper.Property<ZW>(x => x.IssueWarehouseId)
                .CanSort()
                .CanFilter();
            mapper.Property<ZW>(x => x.IsApproved)
                .CanSort()
                .CanFilter();
            mapper.Property<ZW>(x => x.DepartmentName)
                .CanSort()
                .CanFilter();
        }
    }
}
