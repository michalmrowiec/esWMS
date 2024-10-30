using esWMS.Infrastructure;
using NLog.Config;

namespace esWMS.API.IntegrationTests.Helpers
{
    internal static class DatabaseUtils
    {
        private static void ClearDb(
            this EsWmsDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        internal static async Task CreateDataAsync<TEntity>(
            this EsWmsDbContext context,
            IEnumerable<TEntity> items)
            where TEntity : class
        {
            context.ClearDb();

            await context.Set<TEntity>().AddRangeAsync(items);
            await context.SaveChangesAsync();
        }
    }
}
