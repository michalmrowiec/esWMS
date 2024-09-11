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

        public async Task<IList<WarehouseUnit>> SetWarehouseUnitsBlockedStatusAsync(bool block, params string[] warehouseUnitIds)
        {
            try
            {
                var warehouseUnits = await GetWarehouseUnitsWithItemsByIdsAsync(warehouseUnitIds);

                foreach (var wu in warehouseUnits)
                {
                    foreach (var wui in wu.WarehouseUnitItems)
                    {
                        // Jeśli blokujemy, ustaw BlockedQuantity na ilość. Jeśli odblokowujemy, ustaw na 0.
                        wui.BlockedQuantity = block ? wui.Quantity : 0;
                    }

                    // Ustawienie statusu IsBlocked na podstawie parametru block.
                    wu.IsBlocked = block;
                }

                await _context.SaveChangesAsync();
                return warehouseUnits;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting warehouse units blocked status");
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

        public async Task<IList<WarehouseUnit>> GetWarehouseUnitsWithItemsByIdsAsync(params string[] warehouseUnitIds)
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

        public async Task<IList<WarehouseUnit>> GetAllStackOfWarehouseUnits(string baseWarehouseUnitId)
        {
            List<WarehouseUnit> stack = new List<WarehouseUnit>();
            try
            {
                var result = await _context.WarehouseUnits.FirstOrDefaultAsync(x => x.WarehouseUnitId.Equals(baseWarehouseUnitId));

                if (result == null)
                {
                    throw new KeyNotFoundException("The object with the given id was not found.");
                }

                stack.Add(result);

                await LoadStackOnRecursively(result, stack);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving entity with Id: {EntityId}", baseWarehouseUnitId);
                throw;
            }
            return stack;
        }

        private async Task LoadStackOnRecursively(WarehouseUnit warehouseUnit, List<WarehouseUnit> stack)
        {
            var stackOn = await _context.WarehouseUnits
                .Include(x => x.StackOn)
                .FirstOrDefaultAsync(x => x.StackOnId == warehouseUnit.WarehouseUnitId);

            if (stackOn != null)
            {
                stack.Add(stackOn);
                await LoadStackOnRecursively(stackOn, stack);
            }
        }
    }
}
