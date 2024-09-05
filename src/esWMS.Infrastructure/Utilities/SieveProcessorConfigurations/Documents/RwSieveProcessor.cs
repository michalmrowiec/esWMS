using esMWS.Domain.Entities.Documents;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.Documents
{
    internal class RwSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<RW>(x => x.DocumentId)
                .CanSort()
                .CanFilter();
            mapper.Property<RW>(x => x.IssueWarehouseId)
                .CanSort()
                .CanFilter();
            mapper.Property<RW>(x => x.IsApproved)
                .CanSort()
                .CanFilter();
            mapper.Property<RW>(x => x.DepartmentName)
                .CanSort()
                .CanFilter();
        }
    }
}
