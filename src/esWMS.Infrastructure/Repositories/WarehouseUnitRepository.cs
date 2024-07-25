using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories
{
    internal class WarehouseUnitRepository(EsWmsDbContext context, ILogger<WarehouseUnitRepository> logger)
                : BaseRepository<WarehouseUnit, string>(context, logger), IWarehouseUnitRepository
    { }
}
