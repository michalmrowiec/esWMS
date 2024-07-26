using esMWS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IZoneRepository
        : IBaseRepository<Zone>
    {
        Task<IList<Zone>> GetAllWarehouseZones(string warehouseId);
    }
}
