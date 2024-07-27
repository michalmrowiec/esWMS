using esMWS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IWarehouseUnitRepository
        : IBaseRepository<WarehouseUnit>
    {
        Task<IList<WarehouseUnit>> GetWarehouseUnitWitchItems(string warehouseUnitId);
    }
}
