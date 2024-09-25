using esMWS.Domain.Entities.SystemActors;
using esMWS.Domain.Entities.WarehouseEnviroment;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.SystemActors
{
    internal class ContractorRepository
        (EsWmsDbContext context,
        ILogger<ContractorRepository> logger,
        ISieveProcessor sieveProcessor)
        : BaseRepository<Contractor>(context, logger), IContractorRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;

        public async Task<PagedResult<Contractor>> GetSortedFilteredAsync(SieveModel sieveModel)
        {
            var contractors = _context.Contractors
                .AsNoTracking()
                .AsQueryable();

            var filteredContractors = await _sieveProcessor
                .Apply(sieveModel, contractors)
                .ToListAsync();

            var totalCount = await _sieveProcessor
                .Apply(sieveModel, contractors, applyPagination: false, applySorting: false)
                .CountAsync();

            return new PagedResult<Contractor>(filteredContractors, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
        }
    }
}
