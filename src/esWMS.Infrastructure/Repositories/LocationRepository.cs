using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories
{
    internal class LocationRepository(EsWmsDbContext context, ILogger<LocationRepository> logger)
                : BaseRepository<Location>(context, logger), ILocationRepository
    {
        public Task<IList<Location>> GetAllZoneLocations(string zoneId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Location>> GetBusyZoneLocations(string zoneId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Location>> GetFreeZoneLocations(string zoneId)
        {
            throw new NotImplementedException();
        }
    }
}
