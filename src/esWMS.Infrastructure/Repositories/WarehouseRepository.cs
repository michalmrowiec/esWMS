using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories
{
    internal class WarehouseRepository(EsWmsDbContext context, ILogger<WarehouseRepository> logger)
                : BaseRepository<Warehouse>(context, logger), IWarehouseRepository
    {
        public Task<IList<WarehouseStock>> GetWarehouseStocks(string warehouseId)
        {
            throw new NotImplementedException();
        }
    }
}
