using esWMS.Application.Contracts.Persistence;
using esWMS.Domain.Entities.WarehouseEnvironment;
using esWMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.WarehouseEnvironment
{
    internal class ProductRepository(EsWmsDbContext context,
        ILogger<ProductRepository> logger,
        ISieveProcessor sieveProcessor)
        : BaseRepository<Product>(context, logger), IProductRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;
        private readonly ILogger<ProductRepository> _logger = logger;
        public override async Task<Product> GetByIdAsync(string id)
        {
            try
            {
                var result = await _context.Products
                    .Include(x => x.Category)
                    .FirstOrDefaultAsync(x => x.ProductId.Equals(id));

                return result ?? throw new KeyNotFoundException("The object with the given id was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving entity with Id: {EntityId}", id);
                throw;
            }
        }

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

            return new PagedResult<Product>
                (filteredProducts, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
        }
    }
}
