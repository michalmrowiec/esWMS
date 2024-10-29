using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sieve.Services;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class MmmRepository(EsWmsDbContext context, ILogger<MmmRepository> logger, ISieveProcessor sieveProcessor)
                : BaseDocumentRepository<MMM>(context, logger, sieveProcessor), IMmmRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<MmmRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;

        public override async Task<MMM> GetDocumentByIdWithItemsAsync(string id)
        {
            try
            {
                var result = await _context
                    .Set<MMM>()
                    .Include(x => x.DocumentItems)
                        .ThenInclude(x => x.DocumentWarehouseUnitItems)
                            .ThenInclude(x => x.WarehouseUnitItem)
                                .ThenInclude(x => x.WarehouseUnit)
                    .Include(x => x.DocumentItems)
                        .ThenInclude(x => x.Product)
                    .Include(x => x.RelatedMmp)
                    .Include(x => x.IssueWarehouse)
                    .Include(x => x.ToWarehouse)
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
    }
}
