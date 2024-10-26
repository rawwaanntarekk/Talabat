using LinkDev.Talabat.Core.Domain.Contracts.Persistence;

namespace LinkDev.Talabat.Infrastructure.Persistence._Common
{
    internal abstract class DbInitialzer (DbContext _dbContext) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                await _dbContext.Database.MigrateAsync();
            }
        }

        public  abstract  Task SeedAsync();
    }
}
