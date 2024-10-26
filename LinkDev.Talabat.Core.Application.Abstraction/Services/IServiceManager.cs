﻿

using LinkDev.Talabat.Core.Application.Abstraction.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services
{
	public interface IServiceManager
	{
        public IProductService ProductService { get; }
        public IBasketService BasketService { get; }
        public IAuthService AuthService { get; }


    }
}
