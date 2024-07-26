﻿using esMWS.Domain.Entities.WarehouseEnviroment;
using esWMS.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories
{
    internal class ZoneRepository(EsWmsDbContext context, ILogger<ZoneRepository> logger)
                : BaseRepository<Zone>(context, logger), IZoneRepository
    {
        public Task<IList<Zone>> GetAllWarehouseZones(string warehouseId)
        {
            throw new NotImplementedException();
        }
    }
}
