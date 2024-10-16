using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Middlewares;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.Core.Application.Abstraction.Contracts;
using LinkDev.Talabat.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
namespace LinkDev.Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.

            webApplicationBuilder.Services.AddControllers()
                                          .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly)
                                          .ConfigureApiBehaviorOptions(options =>
                                          {
                                              options.SuppressModelStateInvalidFilter = false;
                                              options.InvalidModelStateResponseFactory = (actionContext) =>
                                              {
                                                  var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
                                                                                       .Select(P => new ApiValidationErrorResponse.ValidationError()
                                                                                       {
                                                                                           Field = P.Key,
                                                                                           Errors = P.Value!.Errors.Select(e => e.ErrorMessage)
                                                                                       });

                                                  return new BadRequestObjectResult(new ApiValidationErrorResponse()
                                                  {
                                                      Errors = errors
                                                  });
                                              };

                                          });
            // Register Required Services for Controllers in the DI Container
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            webApplicationBuilder.Services.AddEndpointsApiExplorer().
                                           AddSwaggerGen();

            webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);

            webApplicationBuilder.Services.AddHttpContextAccessor(); // Register IHttpContextAccessor in the DI Container
            webApplicationBuilder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService)); // Register ILoggedInUserService in the DI Container

            webApplicationBuilder.Services.AddApplicationServices(); // Register Application Services in the DI Container
           
            #endregion

            var app = webApplicationBuilder.Build();

            #region Databases Initialization

            await app.InitializeStoreContextAsync();

            #endregion

            #region Configure Kestrel Middlewares
            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
