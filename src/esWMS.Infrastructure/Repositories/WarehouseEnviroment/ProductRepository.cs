using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.WarehouseEnviroment
{
    internal class ProductRepository(EsWmsDbContext context, ILogger<ProductRepository> logger, ISieveProcessor sieveProcessor)
        : BaseRepository<Product>(context, logger), IProductRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;

        public async Task<PagedResult<Product>> GetSortedFilteredAsync(SieveModel sieveModel)
        {
            var products = _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .AsQueryable();

            var filteredProducts = await _sieveProcessor
                .Apply(sieveModel, products)
                .ToListAsync();

            var totalCount = await _sieveProcessor
                .Apply(sieveModel, products, applyPagination: false, applySorting: false)
                .CountAsync();

            return new PagedResult<Product>(filteredProducts, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
        }
    }
}
