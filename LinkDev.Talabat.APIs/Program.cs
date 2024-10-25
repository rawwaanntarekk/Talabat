using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Middlewares;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.Core.Application.Abstraction.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using LinkDev.Talabat.Infrasturcture;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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
                                                                                       .SelectMany(P => P.Value!.Errors)
                                                                                       .Select(E => E.ErrorMessage);

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

            webApplicationBuilder.Services.AddInfrastructureServices(webApplicationBuilder.Configuration);

            webApplicationBuilder.Services.AddIdentityServices();
           
            #endregion

            var app = webApplicationBuilder.Build();

            #region Databases Initialization

            await app.InitializeDbAsync();

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
