using esWMS.Domain.Entities.WarehouseEnvironment;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IProductRepository
        : IBaseRepository<Product>, ISieve<Product>
    {
    }
}
