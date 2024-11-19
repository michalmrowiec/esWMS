namespace esWMS.Application.Contracts.Persistence
{
    public interface IBaseRepository<TEntity, TId> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TId entityId);
        Task<TEntity> GetByIdAsync(TId id);
        Task<IList<TEntity>> GetAllAsync();
    }

    public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, string>
        where TEntity : class;
}
