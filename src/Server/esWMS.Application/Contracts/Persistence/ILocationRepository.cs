using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface ILocationRepository
        : IBaseRepository<Location>, ISieve<Location>
    {
        Task<IList<Location>> GetAllZoneLocations(string zoneId);
        Task<IList<Location>> GetBusyZoneLocations(string zoneId);
        Task<IList<Location>> GetFreeZoneLocations(string zoneId);
    }
}
