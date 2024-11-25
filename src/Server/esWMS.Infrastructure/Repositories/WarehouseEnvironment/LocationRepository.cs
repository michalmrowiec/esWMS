using esWMS.Application.Contracts.Persistence;
using esWMS.Domain.Entities.WarehouseEnvironment;
using esWMS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.WarehouseEnvironment
{
    internal class LocationRepository
        (EsWmsDbContext context,
        ILogger<LocationRepository> logger,
        ISieveProcessor sieveProcessor)
        : BaseRepository<Location>(context, logger), ILocationRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;
        private readonly ILogger<LocationRepository> _logger = logger;

        public Task<IList<Location>> GetAllZoneLocations(string zoneId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Location>> GetBusyZoneLocations(string zoneId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Location>> GetFreeZoneLocations(string zoneId)
        {
            throw new NotImplementedException();
        }

        public override async Task<Location> GetByIdAsync(string id)
        {
            try
            {
                var result = await _context.Locations
                    .Include(x => x.WarehouseUnits)
                    .FirstOrDefaultAsync(x => x.LocationId.Equals(id));
                return result ?? throw new KeyNotFoundException("The object with the given id was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving entity with Id: {EntityId}", id);
                throw;
            }
        }

        public async Task<PagedResult<Location>> GetSortedFilteredAsync(SieveModel sieveModel)
        {
            var locations = _context.Locations
                            .Include(x => x.Zone)
                                .ThenInclude(x => x != null ? x.Warehouse : null)
                            .Include(x => x.WarehouseUnits)
                            .AsNoTracking()
                            .AsQueryable();

            var filteredLocations = await _sieveProcessor
                .Apply(sieveModel, locations)
                .ToListAsync();

            var totalCount = await _sieveProcessor
                .Apply(sieveModel, locations, applyPagination: false, applySorting: false)
                .CountAsync();

            return new PagedResult<Location>(filteredLocations, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
        }
    }
}
