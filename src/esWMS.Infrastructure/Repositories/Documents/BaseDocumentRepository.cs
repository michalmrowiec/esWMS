using esMWS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class BaseDocumentRepository<TDocument>
        : BaseRepository<TDocument>, IBaseDocumentRepository<TDocument>
        where TDocument : BaseDocument
    {
        private readonly EsWmsDbContext _context;
        private readonly ILogger<BaseDocumentRepository<TDocument>> _logger;

        public BaseDocumentRepository
            (EsWmsDbContext context, ILogger<BaseDocumentRepository<TDocument>> logger)
            : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

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

        public Task<int> GetLastDocumentNumberOfDay(DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}
