using jex_assessment.Data;
using Microsoft.EntityFrameworkCore;

namespace jex_assessment.Extensions
{
    public static class DatabaseMigrationExtensions
    {
        /// <summary>
        /// Applies any pending EF Core migrations to the database at application startup.
        /// </summary>
        /// <param name="app">The WebApplication instance.</param>
        /// <returns>The WebApplication instance (for chaining).</returns>
        public static WebApplication ApplyDatabaseMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var logger = scope
                .ServiceProvider.GetService<ILoggerFactory>()
                ?.CreateLogger("DatabaseMigration");
            try
            {
                var db = scope.ServiceProvider.GetRequiredService<AssessmentDbContext>();
                var pendingMigrations = db.Database.GetPendingMigrations().ToList();
                if (pendingMigrations.Count == 0)
                {
                    logger?.LogInformation("No database migrations to apply.");
                }
                else
                {
                    db.Database.Migrate();
                    logger?.LogInformation(
                        "Database migrations applied successfully: {Migrations}",
                        string.Join(", ", pendingMigrations)
                    );
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "An error occurred while applying database migrations.");
                throw;
            }
            return app;
        }
    }
}
