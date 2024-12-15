using esWMS.Domain.Entities.WarehouseEnvironment;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.WarehouseEnviroment
{
    internal class WarehouseSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Warehouse>
                (x => x.WarehouseId)
                .CanSort()
                .CanFilter();
            mapper.Property<Warehouse>(x => x.WarehouseName)
                .CanSort()
                .CanFilter();
            mapper.Property<Warehouse>(x => x.City)
                .CanSort()
                .CanFilter();
            mapper.Property<Warehouse>(x => x.Address)
                .CanSort()
                .CanFilter();
            mapper.Property<Warehouse>(x => x.PostalCode)
                .CanSort()
                .CanFilter();
            mapper.Property<Warehouse>(x => x.Region)
                .CanSort()
                .CanFilter();
            mapper.Property<Warehouse>(x => x.IsActive)
                .CanSort()
                .CanFilter();
        }
    }
}
