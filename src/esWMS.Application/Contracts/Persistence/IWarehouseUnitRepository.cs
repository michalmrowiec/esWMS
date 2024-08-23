using esMWS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IWarehouseUnitRepository
        : IBaseRepository<WarehouseUnit>, ISieve<WarehouseUnit>
    {
        Task<IList<WarehouseUnit>> GetWarehouseUnitsWithItemsByIdAsync(params string[] warehouseUnitIds);
        Task<IList<WarehouseUnit>> UpdateWarehouseUnitsAsync(params WarehouseUnit[] warehouseUnits);
        Task<IList<WarehouseUnit>> CreateRangeAsync(IEnumerable<WarehouseUnit> warehouseUnits);
        Task<IList<WarehouseUnit>> BlockWarehouseUnitsWithAllItemsAsync(params string[] warehouseUnitIds);
    }
}
