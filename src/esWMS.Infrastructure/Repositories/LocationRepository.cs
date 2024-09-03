using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories
{
    internal class LocationRepository(EsWmsDbContext context, ILogger<LocationRepository> logger, ISieveProcessor sieveProcessor)
                : BaseRepository<Location>(context, logger), ILocationRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;

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

        public async Task<PagedResult<Location>> GetSortedFilteredAsync(SieveModel sieveModel)
        {
            var locations = _context.Locations
                            .Include(x => x.Zone)
                                .ThenInclude(x => x != null ? x.Warehouse : null)
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
