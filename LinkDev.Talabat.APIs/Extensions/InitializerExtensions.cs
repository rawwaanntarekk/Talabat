﻿using LinkDev.Talabat.Core.Domain.Contracts;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class InitializerExtensions
    {
        public static async Task<WebApplication> InitializeStoreContextAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var storeContextInitializer = services.GetRequiredService<IStoreContextInitializer>();
            // Ask Rutime for an object of type StoreContext  "Explicitly"

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await storeContextInitializer.InitializeAsync();
                await storeContextInitializer.SeedAsync();

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred while migrating or seeding  database.");
            }

            return app;
        }
    }
}
