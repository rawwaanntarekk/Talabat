using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Mapping;
using LinkDev.Talabat.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Core.Application
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{

			services.AddAutoMapper(typeof(MappingProfile));

			services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

			// services.AddScoped(typeof(IBasketService), typeof(BasketService));


			services.AddScoped(typeof(Func<IBasketService>), (servicesProvider) =>
			{
				//var mapper = servicesProvider.GetRequiredService<IMapper>();
				//var configuration = servicesProvider.GetRequiredService<IConfiguration>();
				//var basketRepository = servicesProvider.GetRequiredService<IBasketRepository>();

				//return () => new BasketService(basketRepository, mapper, configuration);

				return () => servicesProvider.GetRequiredService<IBasketService>();

			});

			return services;
		}
	}
}
