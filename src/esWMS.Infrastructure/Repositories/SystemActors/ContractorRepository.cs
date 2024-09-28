using esWMS.Application.Contracts.Persistence;
using esWMS.Domain.Entities.SystemActors;
using esWMS.Domain.Models;
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
        private readonly ILogger<ContractorRepository> _logger = logger;

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

        public override async Task<Contractor> GetByIdAsync(string id)
        {
            try
            {
                var result = await _context.Contractors
                    .Include(x => x.PZDocuments)
                    .Include(x => x.WZDocuments)
                    .FirstOrDefaultAsync(x => x.ContractorId.Equals(id));

                return result ?? throw new KeyNotFoundException("The object with the given id was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving entity with Id: {EntityId}", id);
                throw;
            }
        }
    }
}
