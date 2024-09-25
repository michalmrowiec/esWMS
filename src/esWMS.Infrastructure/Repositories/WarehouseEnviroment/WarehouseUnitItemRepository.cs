using esWMS.Domain.Entities.WarehouseEnviroment;
using esWMS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.WarehouseEnviroment
{
    internal class WarehouseUnitItemRepository
        (EsWmsDbContext context,
        ILogger<WarehouseUnitItemRepository> logger,
        ISieveProcessor sieveProcessor)
        : BaseRepository<WarehouseUnitItem>(context, logger), IWarehouseUnitItemRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<WarehouseUnitItemRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;

        public async Task<IList<WarehouseUnitItem>> BlockExistWarehouseUnitItemsQuantityAsync
            (Dictionary<string, int> warehouseUnitItemIdQuantity)
        {
            try
            {
                var warehouseUnitItems = await GetWarehouseUnitItemsByIdsAsync(warehouseUnitItemIdQuantity.Keys.ToArray());

                foreach (var item in warehouseUnitItems)
                {
                    // TODO check the blocked quantity???

                    item.BlockedQuantity += warehouseUnitItemIdQuantity[item.WarehouseUnitItemId];
                }

                await _context.SaveChangesAsync();
                return warehouseUnitItems;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error blocking warehouse unit items quantity");
                throw;
            }
        }

        public async Task<IList<WarehouseUnitItem>> CreateRangeAsync(IList<WarehouseUnitItem> warehouseUnitItems)
        {
            try
            {
                await _context.WarehouseUnitItems.AddRangeAsync(warehouseUnitItems);
                await _context.SaveChangesAsync();
                return warehouseUnitItems;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating entity");
                throw;
            }
        }

        public async Task<PagedResult<WarehouseUnitItem>> GetSortedFilteredAsync(SieveModel sieveModel)
        {
            try
            {
                var items = _context
                    .WarehouseUnitItems
                    .Include(x => x.Product)
                    .Include(x => x.WarehouseUnit)
                    .Include(x => x.DocumentWarehouseUnitItems)
                    .AsNoTracking()
                    .AsQueryable();

                var filteredItems = await _sieveProcessor
                    .Apply(sieveModel, items)
                    .ToListAsync();

                var totalCount = await _sieveProcessor
                    .Apply(sieveModel, items, applyPagination: false, applySorting: false)
                    .CountAsync();

                return new PagedResult<WarehouseUnitItem>(filteredItems, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving warehouse stocks");
                throw;
            }
        }

        public async Task<IList<WarehouseUnitItem>> GetWarehouseUnitItemsByIdsAsync
            (params string[] warehouseUnitItemsIds)
        {
            try
            {
                return await _context.WarehouseUnitItems
                    .Where(wui => warehouseUnitItemsIds.Contains(wui.WarehouseUnitItemId))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving warehouse unit items");
                throw;
            }
        }

        public async Task<IList<WarehouseUnitItem>> UpdateWarehouseUnitItemsAsync
            (params WarehouseUnitItem[] warehouseUnitItems)
        {
            try
            {
                _context.WarehouseUnitItems.UpdateRange(warehouseUnitItems);
                await _context.SaveChangesAsync();
                return warehouseUnitItems;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating warehouse unit items");
                throw;
            }
        }
    }
}
