using esWMS.Domain.Entities.Documents;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.Documents
{
    internal class PwSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<PW>(x => x.DocumentId)
                .CanSort()
                .CanFilter();
            mapper.Property<PW>(x => x.IssueWarehouseId)
                .CanSort()
                .CanFilter();
            mapper.Property<PW>(x => x.IsApproved)
                .CanSort()
                .CanFilter();
            mapper.Property<PW>(x => x.DepartmentName)
                .CanSort()
                .CanFilter();
        }
    }
}
