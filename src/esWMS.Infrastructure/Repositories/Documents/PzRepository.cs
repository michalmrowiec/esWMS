using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class PzRepository(EsWmsDbContext context, ILogger<PzRepository> logger, ISieveProcessor sieveProcessor)
                : BaseDocumentRepository<PZ>(context, logger, sieveProcessor), IPzRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<PzRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;

        public override async Task<PagedResult<PZ>> GetSortedFilteredAsync(SieveModel sieveModel)
        {
            var documents = _context
                .Set<PZ>()
                .Include(x => x.IssueWarehouse)
                .Include(x => x.DocumentItems)
                    .ThenInclude(x => x.DocumentWarehouseUnitItems)
                .Include(x => x.SupplierContractor)
                .AsNoTracking()
                .AsQueryable();

            var filteredDocuments = await _sieveProcessor
                .Apply(sieveModel, documents)
                .ToListAsync();

            var totalCount = await _sieveProcessor
                .Apply(sieveModel, documents, applyPagination: false, applySorting: false)
                .CountAsync();

            return new PagedResult<PZ>(filteredDocuments, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
        }
    }
}
