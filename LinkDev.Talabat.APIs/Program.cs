
using LinkDev.Talabat.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.APIs
{
    public class Program
    {
        public static void Main(string[] args)
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
