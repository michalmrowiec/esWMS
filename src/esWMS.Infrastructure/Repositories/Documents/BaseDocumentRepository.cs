using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Models;
using esWMS.Application.Contracts.Persistence.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Models;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class BaseDocumentRepository<TDocument>
        (EsWmsDbContext context,
        ILogger<BaseDocumentRepository<TDocument>> logger,
        ISieveProcessor sieveProcessor)
        : BaseRepository<TDocument>(context, logger), IBaseDocumentRepository<TDocument>
        where TDocument : BaseDocument
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<BaseDocumentRepository<TDocument>> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;

        private readonly Dictionary<Type, Func<IQueryable<TDocument>, IQueryable<TDocument>>> _queryStrategies =
            new()
            {
                { typeof(WZ), (query) => (IQueryable<TDocument>)((IQueryable<WZ>)query)
                    .Include(x => x.RecipientContractor)
                },
                { typeof(PZ), (query) => (IQueryable<TDocument>)((IQueryable<PZ>)query)
                    .Include(x => x.SupplierContractor)
                },
                { typeof(MMM), (query) => (IQueryable<TDocument>)((IQueryable<MMM>)query)
                    .Include(x => x.ToWarehouse)
                    .Include(x => x.RelatedMmp)
                },
                { typeof(MMP), (query) => (IQueryable<TDocument>)((IQueryable<MMP>)query)
                    .Include(x => x.FromWarehouse)
                    .Include(x => x.RelatedMmm)
                },
                { typeof(BaseDocument), (query) => query
                    .Include(x => x.IssueWarehouse)
                    .Include(x => x.DocumentItems)
                        .ThenInclude(x => x.DocumentWarehouseUnitItems)
                }
            };

        public async Task<TDocument> GetDocumentByIdWithItemsAsync(string id)
        {
            try
            {
                var result = await _context
                    .Set<TDocument>()
                    .Include(x => x.DocumentItems)
                        .ThenInclude(x => x.DocumentWarehouseUnitItems)
                            .ThenInclude(x => x.WarehouseUnitItem)
                                .ThenInclude(x => x.WarehouseUnit)
                    .FirstOrDefaultAsync(x => x.DocumentId.Equals(id));

                return result ?? throw new KeyNotFoundException("The object with the given id was not found.");
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning("Document with Id: {DocumentId} was not found.", id);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving entity with Id: {DocumentId}", id);
                throw;
            }
        }

        public async Task<string[]> GetAllDocumentIdForDay(DateTime date)
        {
            try
            {
                var result = await _context
                    .Set<TDocument>()
                    .AsNoTracking()
                    .Where(x => x.DocumentIssueDate.Date.Equals(date.Date))
                    .Select(x => x.DocumentId)
                    .ToArrayAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving document for date: {Date}", date.Date);
                throw;
            }
        }

        public virtual async Task<PagedResult<TDocument>> GetSortedFilteredAsync(SieveModel sieveModel)
        {
            var documents = _context
                .Set<TDocument>()
                .AsNoTracking();

            if (_queryStrategies.TryGetValue(typeof(TDocument), out var strategy))
            {
                documents = strategy(documents);
            }

            documents = _queryStrategies[typeof(BaseDocument)](documents);

            var filteredDocuments = await _sieveProcessor
                .Apply(sieveModel, documents)
                .ToListAsync();

            var totalCount = await _sieveProcessor
                .Apply(sieveModel, documents, applyPagination: false, applySorting: false)
                .CountAsync();

            return new PagedResult<TDocument>(filteredDocuments, totalCount, sieveModel.PageSize.Value, sieveModel.Page.Value);
        }
    }
}
