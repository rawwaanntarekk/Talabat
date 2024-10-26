using LinkDev.Talabat.Core.Application.Abstraction.Auth;
using LinkDev.Talabat.Core.Application.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class IdentityExtenstions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration _configuration)
        {
            services.Configure<JwtSettings>(_configuration.GetSection("jwtSettings"));
            services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions =>
            {
                identityOptions.User.RequireUniqueEmail = true;
                identityOptions.SignIn.RequireConfirmedPhoneNumber = true;
                identityOptions.SignIn.RequireConfirmedAccount = true;
                identityOptions.SignIn.RequireConfirmedEmail = true;


                //identityOptions.Password.RequireNonAlphanumeric = true;
                //identityOptions.Password.RequiredUniqueChars = 2;
                //identityOptions.Password.RequiredLength = 6;
                //identityOptions.Password.RequireDigit = true;
                //identityOptions.Password.RequireLowercase = true;
                //identityOptions.Password.RequireUppercase = true;


                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 10;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);



            })
            .AddEntityFrameworkStores<StoreIdentityDbContext>();

            services.AddScoped(typeof(IAuthService), typeof(AuthService));

            services.AddScoped(typeof(Func<IAuthService>), (servicePovidor) =>
            {
                return () => servicePovidor.GetRequiredService<IAuthService>();
            });

            services.AddAuthentication((authenticationOptions) =>
            {
                authenticationOptions.DefaultAuthenticateScheme = "MySchema";
            }).AddJwtBearer("MySchema", (configurationsOptions) =>
            {
                configurationsOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.FromMinutes(10),
                    ValidIssuer = _configuration.GetSection("jwtSettings")["Issuer"],
                    ValidAudience = _configuration.GetSection("jwtSettings")["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("jwtSettings")["Key"]!))



                };

                configurationsOptions.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        var authorizationHeader = context.Request.Headers["Authorization"].ToString();

                        if (authorizationHeader.StartsWith("MySchema", StringComparison.OrdinalIgnoreCase))
                            context.Token = authorizationHeader.Substring("MySchema".Length).Trim();

                        return Task.CompletedTask;
                    }
                };

            });
            
            return services;

        }
    }
}
