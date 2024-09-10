using esMWS.Domain.Entities.WarehouseEnviroment;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.WarehouseEnviroment
{
    class LocationSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Location>(x => x.LocationId)
                .CanSort()
                .CanFilter();
            mapper.Property<Location>(x => x.ZoneId)
                .CanSort()
                .CanFilter();
            mapper.Property<Location>(x => x.Zone.WarehouseId)
                .CanSort()
                .CanFilter()
                .HasName("WarehouseId");
            mapper.Property<Location>(x => x.Zone.ZoneName)
                .CanSort()
                .CanFilter()
                .HasName("ZoneName");
            mapper.Property<Location>(x => x.Zone.ZoneAlias)
                .CanSort()
                .CanFilter()
                .HasName("ZoneAlias");
            mapper.Property<Location>(x => x.Capacity)
                .CanSort()
                .CanFilter();
        }
    }
}
