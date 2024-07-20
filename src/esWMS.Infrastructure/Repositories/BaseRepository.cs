using esWMS.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace esWMS.Infrastructure.Repositories
{
    internal class BaseRepository<TEntity, TId, TLogger>(EsWmsDbContext context, ILogger<TLogger> logger)
        : IBaseRepository<TEntity, TId>
        where TEntity : class
        where TLogger : class
    {
        private readonly EsWmsDbContext _context = context;
        private readonly ILogger<TLogger> _logger = logger;

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating entity");
                throw;
            }
        }

        public async Task DeleteAsync(TId entityId)
        {
            try
            {
                var entitySet = _context.Set<TEntity>();
                var existingEntity = await entitySet.FindAsync(entityId)
                                    ?? throw new KeyNotFoundException($"Entity with Id {entityId} not found");

                entitySet.Remove(existingEntity);

                if (await _context.SaveChangesAsync() == 0)
                {
                    throw new InvalidOperationException($"Failed to delete entity with Id {entityId}");
                }
            }
            catch (KeyNotFoundException knfEx)
            {
                _logger.LogError(knfEx, "Error deleting entity with Id: {EntityId} - Entity not found", entityId);
                throw;
            }
            catch (InvalidOperationException ioEx)
            {
                _logger.LogError(ioEx, "Error deleting entity with Id: {EntityId} - Deletion failed", entityId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error deleting entity with Id: {EntityId}", entityId);
                throw;
            }
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            try
            {
                return await _context.Set<TEntity>()
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all entities");
                throw;
            }
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            try
            {
                var result = await _context.Set<TEntity>().FindAsync(id);
                return result ?? throw new KeyNotFoundException("The object with the given id was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving entity with Id: {EntityId}", id);
                throw;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating entity");
                throw;
            }
        }
    }

}