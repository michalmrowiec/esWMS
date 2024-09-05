using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories
{
    internal class ZoneRepository
        (EsWmsDbContext context,
        ILogger<ZoneRepository> logger,
        ISieveProcessor sieveProcessor)
        : BaseRepository<Zone>(context, logger), IZoneRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<ZoneRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;

        public async Task<IList<Zone>> GetAllWarehouseZones(string warehouseId)
        {
            try
            {
                var result = await _context.Zones
                    .Include(x => x.Locations)
                    .Where(x => x.WarehouseId == warehouseId)
                    .ToListAsync();

                return result ?? throw new KeyNotFoundException("The objects with the given WarehouseId was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving zones with WarehouseId: {EntityId}", warehouseId);
                throw;
            }
        }

        public async Task<PagedResult<Zone>> GetSortedFilteredAsync(SieveModel sieveModel)
        {
            var zones = _context.Zones
                .Include(x => x.Locations)
                .Include(x => x.Warehouse)
                .AsNoTracking()
                .AsQueryable();

            var filteredZones = await _sieveProcessor
                .Apply(sieveModel, zones)
                .ToListAsync();

            var totalCount = await _sieveProcessor
                .Apply(sieveModel, zones, applyPagination: false, applySorting: false)
                .CountAsync();

            return new PagedResult<Zone>(filteredZones, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
        }
    }
}
