using esWMS.Domain.Entities.Documents;
using esWMS.Application.Contracts.Persistence.Documents;
using Microsoft.Extensions.Logging;
using Sieve.Services;
using Microsoft.EntityFrameworkCore;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class MmpRepository(EsWmsDbContext context, ILogger<MmpRepository> logger, ISieveProcessor sieveProcessor)
                : BaseDocumentRepository<MMP>(context, logger, sieveProcessor), IMmpRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<MmpRepository> _logger = logger;
        private readonly ISieveProcessor _sieveProcessor = sieveProcessor;

        public override async Task<MMP> GetDocumentByIdWithItemsAsync(string id)
        {
            try
            {
                var result = await _context
                    .Set<MMP>()
                    .Include(x => x.DocumentItems)
                        .ThenInclude(x => x.DocumentWarehouseUnitItems)
                            .ThenInclude(x => x.WarehouseUnitItem)
                                .ThenInclude(x => x.WarehouseUnit)
                    .Include(x => x.DocumentItems)
                        .ThenInclude(x => x.Product)
                    .Include(x => x.RelatedMmm)
                    .Include(x => x.IssueWarehouse)
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
