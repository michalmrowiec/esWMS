using esMWS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface ILocationRepository
        : IBaseRepository<Location>
    {
        Task<IList<Location>> GetAllZoneLocations(string zoneId);
        Task<IList<Location>> GetBusyZoneLocations(string zoneId);
        Task<IList<Location>> GetFreeZoneLocations(string zoneId);
    }
}
