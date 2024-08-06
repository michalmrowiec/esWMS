using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using esWMS.Infrastructure.Repositories.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories
{
    internal class WarehouseRepository
        (EsWmsDbContext context,
        ILogger<WarehouseRepository> logger,
        ISieveProcessor sieveProcessor)
        : BaseRepository<Warehouse>(context, logger), IWarehouseRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<WarehouseRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;
        public async Task<PagedResult<Warehouse>> GetSortedFilteredAsync(SieveModel sieveModel)
        {
            var warehouses = _context
                .Warehouses
                .Include(x => x.Zones)
                .AsNoTracking()
                .AsQueryable();

            var filteredWarehouses = await _sieveProcessor
                .Apply(sieveModel, warehouses)
                .ToListAsync();

            var totalCount = await _sieveProcessor
                .Apply(sieveModel, warehouses, applyPagination: false, applySorting: false)
                .CountAsync();

            return new PagedResult<Warehouse>(filteredWarehouses, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
        }

        public Task<IList<WarehouseStock>> GetWarehouseStocks(string warehouseId)
        {
            throw new NotImplementedException();
        }
    }
}
