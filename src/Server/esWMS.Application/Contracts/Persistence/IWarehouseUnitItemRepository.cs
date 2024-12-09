using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IWarehouseUnitItemRepository
        : IBaseRepository<WarehouseUnitItem>, ISieve<WarehouseUnitItem>
    {
        Task<IList<WarehouseUnitItem>> GetWarehouseUnitItemsByIdsAsync(params string[] warehouseUnitItemsIds);
        Task<IList<WarehouseUnitItem>> UpdateWarehouseUnitItemsAsync(params WarehouseUnitItem[] warehouseUnitItems);
        Task<IList<WarehouseUnitItem>> BlockExistWarehouseUnitItemsQuantityAsync(Dictionary<string, decimal> warehouseUnitItemIdQuantity);
        Task<IList<WarehouseUnitItem>> CreateRangeAsync(IList<WarehouseUnitItem> warehouseUnitItems);
    }
}
