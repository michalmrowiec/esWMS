using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories
{
    internal class ProductRepository(EsWmsDbContext context, ILogger<ProductRepository> logger)
        : BaseRepository<Product, string, ProductRepository>(context, logger), IProductRepository
    { }
}
