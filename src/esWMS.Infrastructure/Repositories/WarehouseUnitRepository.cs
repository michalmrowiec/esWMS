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
                        wui.BlockedQuantity = block ? wui.Quantity : 0;
                    }

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
                        .ThenInclude(wui => wui.Product)
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

        public async Task<IList<WarehouseUnit>> GetStackedWarehouseUnitsAboveAsync(string warehouseUnitId)
        {
            List<WarehouseUnit> stackAbove = new List<WarehouseUnit>();
            try
            {
                var result = await _context.WarehouseUnits
                    .Include(x => x.WarehouseUnitItems)
                        .ThenInclude(x => x.Product)
                    .FirstOrDefaultAsync(x => x.WarehouseUnitId.Equals(warehouseUnitId));

                if (result == null)
                {
                    throw new KeyNotFoundException("The object with the given id was not found.");
                }

                stackAbove.Add(result);

                await LoadStackAboveOnIteratively(result, stackAbove);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving entity with Id: {EntityId}", warehouseUnitId);
                throw;
            }
            return stackAbove;
        }

        private async Task LoadStackAboveOnIteratively(WarehouseUnit warehouseUnit, List<WarehouseUnit> stack)
        {
            var currentUnit = warehouseUnit;
            while (currentUnit != null)
            {
                var nextUnit = await _context.WarehouseUnits
                    .Include(x => x.WarehouseUnitItems)
                        .ThenInclude(x => x.Product)
                    .FirstOrDefaultAsync(x => x.StackOnId == currentUnit.WarehouseUnitId);

                if (nextUnit == null) break;

                if (!stack.Contains(nextUnit))
                {
                    stack.Add(nextUnit);
                }

                currentUnit = nextUnit;
            }
        }

        private async Task LoadStackBelowOnIteratively(WarehouseUnit warehouseUnit, List<WarehouseUnit> stack)
        {
            var currentUnit = warehouseUnit;
            while (currentUnit?.StackOnId != null)
            {
                var nextUnit = await _context.WarehouseUnits
                    .Include(x => x.WarehouseUnitItems)
                        .ThenInclude(x => x.Product)
                    .FirstOrDefaultAsync(x => x.WarehouseUnitId == currentUnit.StackOnId);

                if (nextUnit == null) break;

                if (!stack.Contains(nextUnit))
                {
                    stack.Add(nextUnit);
                }

                currentUnit = nextUnit;
            }
        }

        public async Task<IList<WarehouseUnit>> GetFullWarehouseUnitStackAsync(string warehouseUnitId)
        {
            List<WarehouseUnit> fullStack = new List<WarehouseUnit>();
            try
            {
                var result = await _context.WarehouseUnits
                    .Include(x => x.WarehouseUnitItems)
                        .ThenInclude(x => x.Product)
                    .FirstOrDefaultAsync(x => x.WarehouseUnitId.Equals(warehouseUnitId));

                if (result == null)
                {
                    throw new KeyNotFoundException("The object with the given id was not found.");
                }

                fullStack.Add(result);

                await LoadStackBelowOnIteratively(result, fullStack);
                await LoadStackAboveOnIteratively(result, fullStack);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving entity with Id: {EntityId}", warehouseUnitId);
                throw;
            }
            return fullStack;
        }
    }
}
