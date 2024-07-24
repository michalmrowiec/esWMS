using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories
{
    internal class WarehouseRepository(EsWmsDbContext context, ILogger<WarehouseRepository> logger)
                : BaseRepository<Warehouse, string>(context, logger), IWarehouseRepository
    { }
}
