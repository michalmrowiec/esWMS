using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories
{
    internal class ZoneRepository(EsWmsDbContext context, ILogger<ZoneRepository> logger)
                : BaseRepository<Zone, string>(context, logger), IZoneRepository
    { }
}
