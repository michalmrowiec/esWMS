using esMWS.Domain.Entities.WarehouseEnviroment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IWarehouseUnitRepository
        : IBaseRepository<WarehouseUnit>
    {
        Task<IList<WarehouseUnit>> GetWarehouseUnitsWithItemsByIdAsync(params string[] warehouseUnitIds);
        Task<IList<WarehouseUnit>> UpdateWarehouseUnitsAsync(params WarehouseUnit[] warehouseUnits);
    }
}
