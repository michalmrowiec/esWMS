using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IWarehouseUnitRepository
        : IBaseRepository<WarehouseUnit>, ISieve<WarehouseUnit>
    {
        Task<IList<WarehouseUnit>> GetWarehouseUnitsWithItemsByIdsAsync(params string[] warehouseUnitIds);
        Task<IList<WarehouseUnit>> UpdateWarehouseUnitsAsync(params WarehouseUnit[] warehouseUnits);
        Task<IList<WarehouseUnit>> CreateRangeAsync(IEnumerable<WarehouseUnit> warehouseUnits);
        Task<IList<WarehouseUnit>> SetWarehouseUnitsBlockedStatusAsync(bool block, params string[] warehouseUnitIds);
        Task<IList<WarehouseUnit>> GetStackedWarehouseUnitsAboveAsync(string warehouseUnitId);
        Task<IList<WarehouseUnit>> GetFullWarehouseUnitStackAsync(string warehouseUnitId);
    }
}
