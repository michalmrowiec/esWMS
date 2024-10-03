using esWMS.Application.Contracts.Persistence.Documents;
using esWMS.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories.Documents
{
    internal class DocumentItemRepository(
        EsWmsDbContext context,
        ILogger<DocumentItemRepository> logger)
        : BaseRepository<DocumentItem>(context, logger), IDocumentItemRepository
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<DocumentItemRepository> _logger = logger;

        public async Task<DocumentItem> GetDocumentItemByIdWithAssignments(string documnetItemId)
        {
            try
            {
                var result = await _context.DocumentItems
                    .Include(x => x.DocumentWarehouseUnitItems)
                        .ThenInclude(x => x.WarehouseUnitItem)
                    .FirstOrDefaultAsync(x => x.DocumentItemId.Equals(documnetItemId));

                return result ?? throw new KeyNotFoundException("The object with the given id was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving entity with Id: {EntityId}", documnetItemId);
                throw;
            }
        }
    }
}
