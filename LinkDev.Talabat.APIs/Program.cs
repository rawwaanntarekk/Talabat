using LinkDev.Talabat.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace LinkDev.Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.

            webApplicationBuilder.Services.AddControllers(); // Register Required Services for Controllers in the DI Container
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer().
                                           AddSwaggerGen();

            webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);

           
            #endregion

            var app = webApplicationBuilder.Build();

            #region Updating Database
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<StoreContext>();
            // Ask Rutime for an object of type StoreContext  "Explicitly"

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred while migrating database.");
            } 
            #endregion

            #region Configure Kestrel Middlewares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
