using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories
{
    internal class WarehouseUnitRepository(EsWmsDbContext context, ILogger<WarehouseUnitRepository> logger)
                : BaseRepository<WarehouseUnit>(context, logger), IWarehouseUnitRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<WarehouseUnitRepository> _logger = logger;
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
