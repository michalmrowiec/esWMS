using esWMS.Application.Contracts.Persistence;
using esWMS.Domain.Entities.WarehouseEnvironment;
using esWMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.WarehouseEnvironment
{
    public class WarehouseUnitItemRepository
        (EsWmsDbContext context,
        ILogger<WarehouseUnitItemRepository> logger,
        ISieveProcessor sieveProcessor)
        : BaseRepository<WarehouseUnitItem>(context, logger), IWarehouseUnitItemRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<WarehouseUnitItemRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;

        public async Task<IList<WarehouseUnitItem>> BlockExistWarehouseUnitItemsQuantityAsync(
            Dictionary<string, decimal> warehouseUnitItemIdQuantity)
        {
            try
            {
                var warehouseUnitItems = await GetWarehouseUnitItemsByIdsAsync(warehouseUnitItemIdQuantity.Keys.ToArray());

                foreach (var item in warehouseUnitItems)
                {
                    var quantityToAdd = warehouseUnitItemIdQuantity[item.WarehouseUnitItemId];

                    if (quantityToAdd < 0)
                        throw new ArgumentOutOfRangeException(nameof(warehouseUnitItemIdQuantity),
                            $"BlockedQuantity for item {item.WarehouseUnitItemId} is out of range. " +
                            $"Attempted to add: {quantityToAdd}");

                    item.BlockedQuantity += quantityToAdd;

                    if (item.BlockedQuantity > item.Quantity || item.BlockedQuantity < 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(warehouseUnitItemIdQuantity),
                            $"BlockedQuantity for item {item.WarehouseUnitItemId} is out of range. " +
                            $"Attempted to add: {quantityToAdd}, Blocked: {item.BlockedQuantity}, Available: {item.Quantity}");
                    }
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

        public async Task DeleteEmptyWarehouseUnitItems()
        {
            try
            {
                var emptyWarehouseUnitItems = _context.WarehouseUnitItems.Where(
                    x => x.Quantity == 0 && x.BlockedQuantity == 0);

                _context.WarehouseUnitItems.RemoveRange(emptyWarehouseUnitItems);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error deleting empty warehouse unit items");
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

        public async Task<IList<WarehouseUnitItem>> GetWarehouseUnitItemsByIdsAsync(
            params string[] warehouseUnitItemsIds)
        {
            try
            {
                return await _context.WarehouseUnitItems
                    .Where(wui => warehouseUnitItemsIds.Contains(wui.WarehouseUnitItemId))
                    .Include(x => x.WarehouseUnit)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving warehouse unit items");
                throw;
            }
        }

        public async Task<IList<WarehouseUnitItem>> UpdateWarehouseUnitItemsAsync(
            params WarehouseUnitItem[] warehouseUnitItems)
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
