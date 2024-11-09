using esWMS.Infrastructure;

namespace esWMS.API.IntegrationTests.Helpers
{
    internal static class DatabaseUtils
    {
        internal static EsWmsDbContext ClearDb(
            this EsWmsDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        internal static async Task CreateDataAsync<TEntity>(
            this EsWmsDbContext context,
            IEnumerable<TEntity> items)
            where TEntity : class
        {
            await context.Set<TEntity>().AddRangeAsync(items);
            await context.SaveChangesAsync();
        }
    }
}
