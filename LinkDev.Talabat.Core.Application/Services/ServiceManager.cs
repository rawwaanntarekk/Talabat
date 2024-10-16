using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        private readonly Lazy<IProductService> _productService;
		private readonly Lazy<IBasketService> _basketService;

		public IProductService ProductService => _productService.Value;

		public IBasketService BasketService => _basketService.Value;

        public ServiceManager(IUnitOfWork unitOfWork , IMapper mapper, IConfiguration configuration, Func<IBasketService> basketServiceFactory)
        {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
            this.configuration = configuration;
            _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
			_basketService = new Lazy<IBasketService>(basketServiceFactory);
		}
    }
}
