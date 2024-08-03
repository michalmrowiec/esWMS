using esMWS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IWarehouseUnitRepository
        : IBaseRepository<WarehouseUnit>
    {
        Task<IList<WarehouseUnit>> GetWarehouseUnitWithItems(string warehouseUnitId);
        Task<IList<WarehouseUnit>> GetWarehouseUnitsByIds(params string[] warehouseUnitIds);
    }
}
