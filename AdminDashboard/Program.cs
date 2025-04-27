using AdminDashboard.Helpers;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region Store Context
            builder.Services.AddDbContext<StoreDbContext>(
                  optionsBuilder =>
                  {
                      optionsBuilder
                      .UseLazyLoadingProxies()
                      .UseSqlServer(builder.Configuration.GetConnectionString("StoreContext"));
                  });

            builder.Services.AddScoped(typeof(IStoreDbInitializer), typeof(StoreDbInitializer));



            #endregion

            #region Identity Context
            builder.Services.AddDbContext<StoreIdentityDbContext>(
                optionsBuilder =>
                {
                    optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(builder.Configuration.GetConnectionString("IdentityContext"));
                });

            builder.Services.AddScoped(typeof(IStoreIdentityDbInitializer), typeof(StoreIdentityDbInitialzer));
            #endregion


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions =>
            {
                identityOptions.User.RequireUniqueEmail = true;
                //identityOptions.SignIn.RequireConfirmedPhoneNumber = true;
                //identityOptions.SignIn.RequireConfirmedAccount = true;
                //identityOptions.SignIn.RequireConfirmedEmail = true;


                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 10;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

            })
            .AddEntityFrameworkStores<StoreIdentityDbContext>();

            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
