using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IWarehouseRepository
        : IBaseRepository<Warehouse>, ISieve<Warehouse>
    {
        Task<IList<WarehouseStock>> GetWarehouseStocks(string warehouseId);
    }
}
