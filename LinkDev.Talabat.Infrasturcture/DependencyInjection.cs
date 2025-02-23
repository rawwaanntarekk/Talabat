using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Infrasturcture.Basket_Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace LinkDev.Talabat.Infrasturcture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
            {
                try
                {
                    var connectionString = configuration.GetConnectionString("Redis");
                    if (string.IsNullOrWhiteSpace(connectionString))
                    {
                        throw new InvalidOperationException("Redis connection string is missing.");
                    }
                    var connectionMultiplexerObj = ConnectionMultiplexer.Connect(connectionString);
                    return connectionMultiplexerObj;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Redis Connection Error: {ex.Message}");
                    throw;
                }
            });

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            return services;


        }
    }
}
