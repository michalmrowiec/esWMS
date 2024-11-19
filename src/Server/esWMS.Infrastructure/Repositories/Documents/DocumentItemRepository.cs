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

        public async Task DeleteRangeAsync(params string[] documentItemIds)
        {
            try
            {
                var entitySet = _context.DocumentItems;
                var existingEntities = await entitySet
                    .Where(x => documentItemIds.Contains(x.DocumentItemId))
                    .ToListAsync();

                if (existingEntities.Count != documentItemIds.Length)
                {
                    throw new KeyNotFoundException($"Not all entities were found. Expected {documentItemIds.Length}, but found {existingEntities.Count}.");
                }

                entitySet.RemoveRange(existingEntities);

                if (await _context.SaveChangesAsync() == 0)
                {
                    throw new InvalidOperationException("Failed to delete entities.");
                }
            }
            catch (KeyNotFoundException knfEx)
            {
                _logger.LogError(knfEx, "Error deleting entities - Some entities not found. Expected IDs: {DocumentItemIds}", string.Join(", ", documentItemIds));
                throw;
            }
            catch (InvalidOperationException ioEx)
            {
                _logger.LogError(ioEx, "Error deleting entities - Deletion failed for IDs: {DocumentItemIds}", string.Join(", ", documentItemIds));
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error deleting entities with IDs: {DocumentItemIds}", string.Join(", ", documentItemIds));
                throw;
            }
        }

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
