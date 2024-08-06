using Microsoft.EntityFrameworkCore;

namespace esWMS.Services
{
    internal static class DatabaseMigrationUpdate
    {
        internal static void UpdateDatabase(this DbContext dbContext, ILogger logger)
        {
            try
            {
                var pendingMigrations = dbContext.Database.GetPendingMigrations();

                if (pendingMigrations.Any())
                {
                    logger.LogInformation("Starting database migration. Pending migrations: {Count}", pendingMigrations.Count());

                    dbContext.Database.Migrate();
                    logger.LogInformation("Database migration completed successfully.");
                }
                else
                {
                    logger.LogInformation("No pending migrations.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database.");
                throw;
            }

        }
    }
}
