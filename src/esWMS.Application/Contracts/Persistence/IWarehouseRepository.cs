using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IWarehouseRepository
        : IBaseRepository<Warehouse>
    {
        Task<IList<WarehouseStock>> GetWarehouseStocks(string warehouseId);
    }
}
