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
            services.AddTransient(typeof(IConnectionMultiplexer), (serviceProvider) =>
            {
                var connectionString = configuration.GetConnectionString("Redis");
                var connectionMultiplexerObj = ConnectionMultiplexer.Connect(connectionString!);
                return connectionMultiplexerObj;
            });

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            return services;
        }
    }
}
