using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories
{
    internal class WarehouseUnitItemRepository(EsWmsDbContext context, ILogger<WarehouseUnitItemRepository> logger)
                : BaseRepository<WarehouseUnitItem>(context, logger), IWarehouseUnitItemRepository
    { }
}
