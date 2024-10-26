using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace LinkDev.Talabat.Infrastructure.Persistence._Identity
{
    public class StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> dbContextOptions,
                                        SaveChangesInterceptor _saveChangesInterceptor) : IdentityDbContext<ApplicationUser>(dbContextOptions)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.ApplyConfiguration(new ApplicationUserConfigurations());
            //builder.ApplyConfiguration(new AddressConfigurations());

            builder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly,
            type => type.GetCustomAttribute<DbContextTypeAttribute>()?.DbContextType == typeof(StoreIdentityDbContext));



        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_saveChangesInterceptor);
        }


    }
}
