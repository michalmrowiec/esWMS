using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories
{
    internal class ZoneRepository : BaseRepository<Zone>, IZoneRepository
    {
        private readonly EsWmsDbContext _context;
        private readonly ILogger<ZoneRepository> _logger;

        public ZoneRepository
            (EsWmsDbContext context, ILogger<ZoneRepository> logger)
            : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

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
    }
}
