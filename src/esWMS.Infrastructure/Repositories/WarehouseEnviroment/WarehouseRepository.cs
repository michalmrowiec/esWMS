using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.WarehouseEnviroment
{
    internal class WarehouseRepository
        (EsWmsDbContext context,
        ILogger<WarehouseRepository> logger,
        ISieveProcessor sieveProcessor)
        : BaseRepository<Warehouse>(context, logger), IWarehouseRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<WarehouseRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;
        public async Task<PagedResult<Warehouse>> GetSortedFilteredAsync(SieveModel sieveModel)
        {
            var warehouses = _context
                .Warehouses
                .Include(x => x.Zones)
                .AsNoTracking()
                .AsQueryable();

            var filteredWarehouses = await _sieveProcessor
                .Apply(sieveModel, warehouses)
                .ToListAsync();

            var totalCount = await _sieveProcessor
                .Apply(sieveModel, warehouses, applyPagination: false, applySorting: false)
                .CountAsync();

            return new PagedResult<Warehouse>(filteredWarehouses, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
        }

        public async Task<PagedResult<WarehouseStock>> GetWarehouseStocks(SieveModel sieveModel, string? warehouseId = null)
        {
            try
            {
                var stockQuery = _context.WarehouseUnitItems
                    .Include(x => x.WarehouseUnit)
                    .Include(x => x.Product)
                        .ThenInclude(x => x.Category)
                    .AsQueryable();

                // TODO refactoring the below to a sieve filter
                if (!string.IsNullOrWhiteSpace(warehouseId))
                {
                    stockQuery = stockQuery.Where(x => x.WarehouseUnit.WarehouseId.Equals(warehouseId));
                }

                var srockQuery2 = stockQuery
                    .GroupBy(x => new
                    {
                        x.ProductId,
                        x.Product.ProductName,
                        x.Product.CategoryId,
                        x.Product.Category.CategoryName,
                    })
                    .Select(g => new WarehouseStock
                    {
                        ProductId = g.Key.ProductId,
                        ProductName = g.Key.ProductName,
                        CategoryId = g.Key.CategoryId,
                        CategoryName = g.Key.CategoryName,
                        Quantity = g.Sum(x => x.Quantity),
                        BlockedQuantity = g.Sum(x => x.BlockedQuantity),
                        Value = g.Sum(x => (x.Price ?? 0) * x.Quantity)
                    })
                    .AsNoTracking()
                    .AsQueryable();

                var filteredWarehouseStocks = await _sieveProcessor
                    .Apply(sieveModel, srockQuery2)
                    .ToListAsync();

                var totalCount = await _sieveProcessor
                    .Apply(sieveModel, srockQuery2, applyPagination: false, applySorting: false)
                    .CountAsync();

                return new PagedResult<WarehouseStock>(filteredWarehouseStocks, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving warehouse stocks");
                throw;
            }
        }
    }
}
