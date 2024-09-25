using esWMS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IZoneRepository
        : IBaseRepository<Zone>, ISieve<Zone>
    {
        Task<IList<Zone>> GetAllWarehouseZones(string warehouseId);
    }
}
