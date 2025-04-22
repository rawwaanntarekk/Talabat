using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Store Context
            services.AddDbContext<StoreDbContext>(
                  optionsBuilder =>
                  {
                      optionsBuilder
                      .UseLazyLoadingProxies()
                      .UseSqlServer(configuration.GetConnectionString("StoreContext"));
                  });

            services.AddScoped(typeof(IStoreDbInitializer), typeof(StoreDbInitializer));

            services.AddScoped(typeof(SaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));
            #endregion

            #region Identity Context
            services.AddDbContext<StoreIdentityDbContext>(
                optionsBuilder =>
                {
                    optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString("IdentityContext"));
                });

            services.AddScoped(typeof(IStoreIdentityDbInitializer), typeof(StoreIdentityDbInitialzer));


            #endregion

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));


            return services;
        }
    }
}
