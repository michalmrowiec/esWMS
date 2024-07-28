using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using Sieve.Models;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IProductRepository
        : IBaseRepository<Product>
    {
        Task<PagedResult<Product>> GetSortedFilteredProductsAsync(SieveModel sieveModel);
    }
}
