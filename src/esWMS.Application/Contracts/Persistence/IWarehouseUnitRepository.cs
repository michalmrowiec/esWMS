using esMWS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IWarehouseUnitRepository
        : IBaseRepository<WarehouseUnit>
    {
        Task<IList<WarehouseUnit>> GetWarehouseUnitWithItemsAsync(string warehouseUnitId);
        Task<IList<WarehouseUnit>> GetWarehouseUnitsByIdsAsync(params string[] warehouseUnitIds);
        Task<IList<WarehouseUnit>> UpdateWarehouseUnitsAsync(params WarehouseUnit[] warehouseUnits);
    }
}
