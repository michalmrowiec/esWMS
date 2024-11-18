using esWMS.Domain.Entities.WarehouseEnviroment;
using Sieve.Services;

namespace esWMS.Infrastructure.Utilities.SieveProcessorConfigurations.WarehouseEnviroment
{
    internal class ZoneSieveProcessor : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Zone>(x => x.ZoneId)
                .CanSort()
                .CanFilter();
            mapper.Property<Zone>(x => x.ZoneName)
                .CanSort()
                .CanFilter();
            mapper.Property<Zone>(x => x.ZoneAlias)
                .CanSort()
                .CanFilter();
            mapper.Property<Zone>(x => x.WarehouseId)
                .CanSort()
                .CanFilter();
            mapper.Property<Zone>(x => x.AvgTemperature)
                .CanSort()
                .CanFilter();
        }
    }
}
