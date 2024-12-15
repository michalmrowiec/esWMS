using esWMS.Domain.Entities.WarehouseEnvironment;
using esWMS.Domain.Models;
using Sieve.Models;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IWarehouseRepository
        : IBaseRepository<Warehouse>, ISieve<Warehouse>
    {
        Task<PagedResult<WarehouseStock>> GetWarehouseStocks(SieveModel sieveModel, string? warehouseId = null);
    }
}
