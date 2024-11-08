using Microsoft.EntityFrameworkCore;

namespace esWMS.Infrastructure.UnitTests.Helpers
{
    public class TestEsWmsDbContext(
        DbContextOptions<EsWmsDbContext> dbContextOptions)
        : EsWmsDbContext(dbContextOptions)
    {
        public override void Dispose()
        {
            Database.EnsureDeleted();
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            Database.EnsureDeletedAsync();
            return base.DisposeAsync();
        }
    }
}