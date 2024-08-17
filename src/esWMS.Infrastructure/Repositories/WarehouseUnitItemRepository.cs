using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories
{
    internal class WarehouseUnitItemRepository(EsWmsDbContext context, ILogger<WarehouseUnitItemRepository> logger)
                : BaseRepository<WarehouseUnitItem>(context, logger), IWarehouseUnitItemRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<WarehouseUnitItemRepository> _logger = logger;

        public async Task<IList<WarehouseUnitItem>> BlockWarehouseUnitItemsQuantityAsync
            (Dictionary<string, int> warehouseUnitItemIdQuantity)
        {
            try
            {
                var warehouseUnitItems = await GetWarehouseUnitItemsByIdsAsync(warehouseUnitItemIdQuantity.Keys.ToArray());

                foreach (var item in warehouseUnitItems)
                {
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
