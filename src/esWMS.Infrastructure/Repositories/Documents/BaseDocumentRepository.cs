using esMWS.Domain.Entities.Documents;
using esMWS.Domain.Entities.WarehouseEnviroment;
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

        public async Task<TDocument> GetDocumentByIdWithItemsAsync(string id)
        {
            try
            {
                var result = await _context
                    .Set<TDocument>()
                    .Include(x => x.DocumentItems)
                    .FirstOrDefaultAsync(x => x.DocumentId == id);

                return result ?? throw new KeyNotFoundException("The object with the given id was not found.");
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

        public Task<BaseDocument> UpdateDocumentAsync(BaseDocument document)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<TDocument>> GetSortedFilteredAsync(SieveModel sieveModel)
        {
            var documents = _context
                .Set<TDocument>()
                .Include(x => x.IssueWarehouse)
                .Include(x => x.DocumentItems)
                .AsNoTracking()
                .AsQueryable();

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
