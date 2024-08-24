using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories
{
    internal class WarehouseUnitRepository
        (EsWmsDbContext context,
        ILogger<WarehouseUnitRepository> logger,
        ISieveProcessor sieveProcessor)
                : BaseRepository<WarehouseUnit>(context, logger), IWarehouseUnitRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<WarehouseUnitRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;

        public async Task<IList<WarehouseUnit>> BlockWarehouseUnitsWithAllItemsAsync(params string[] warehouseUnitIds)
        {
            try
            {
                var warehouseUnits =
                    await GetWarehouseUnitsWithItemsByIdAsync(warehouseUnitIds);

                foreach (var wu in warehouseUnits)
                {
                    foreach (var wui in wu.WarehouseUnitItems)
                    {
                        wui.BlockedQuantity = wui.Quantity;
                    }

                    wu.IsBlocked = true;
                }

                await _context.SaveChangesAsync();
                return warehouseUnits;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error blocking warehouse units with items");
                throw;
            }
        }

        public async Task<IList<WarehouseUnit>> CreateRangeAsync(IEnumerable<WarehouseUnit> warehouseUnits)
        {
            try
            {
                await _context.WarehouseUnits.AddRangeAsync(warehouseUnits);
                await _context.SaveChangesAsync();
                return warehouseUnits.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating entities");
                throw;
            }
        }

        public async Task<PagedResult<WarehouseUnit>> GetSortedFilteredAsync(SieveModel sieveModel)
        {
            var items = _context
                .WarehouseUnits
                .Include(x => x.WarehouseUnitItems)
                    .ThenInclude(x => x.Product)
                .AsNoTracking()
                .AsQueryable();

            var filteredItems = await _sieveProcessor
                .Apply(sieveModel, items)
                .ToListAsync();

            var totalCount = await _sieveProcessor
                .Apply(sieveModel, items, applyPagination: false, applySorting: false)
                .CountAsync();

            return new PagedResult<WarehouseUnit>(filteredItems, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
        }

        public async Task<IList<WarehouseUnit>> GetWarehouseUnitsWithItemsByIdAsync(params string[] warehouseUnitIds)
        {
            try
            {
                return await _context.WarehouseUnits
                    .Where(wu => warehouseUnitIds.Contains(wu.WarehouseUnitId))
                    .Include(wu => wu.WarehouseUnitItems)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving warehouse units");
                throw;
            }
        }

        public async Task<IList<WarehouseUnit>> UpdateWarehouseUnitsAsync(params WarehouseUnit[] warehouseUnits)
        {
            try
            {
                _context.WarehouseUnits.UpdateRange(warehouseUnits);
                await _context.SaveChangesAsync();
                return warehouseUnits;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating warehouse units");
                throw;
            }
        }
    }
}
