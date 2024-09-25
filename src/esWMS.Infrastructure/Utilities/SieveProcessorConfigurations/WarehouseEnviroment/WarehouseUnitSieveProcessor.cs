using esWMS.Domain.Entities.WarehouseEnviroment;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.WarehouseEnviroment
{
    internal class WarehouseUnitSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<WarehouseUnit>(x => x.WarehouseId)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnit>(x => x.WarehouseUnitId)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnit>(x => x.IsBlocked)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnit>(x => x.LocationId)
                .CanSort()
                .CanFilter();
            mapper.Property<WarehouseUnit>(x => x.CanBeStacked)
                .CanSort()
                .CanFilter();
        }
    }
}
