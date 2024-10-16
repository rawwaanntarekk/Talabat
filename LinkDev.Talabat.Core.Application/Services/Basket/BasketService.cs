using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Common.Exceptions;
using LinkDev.Talabat.Core.Domain.Basket;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Basket
{
    internal class BasketService(IBasketRepository basketRepository, IMapper mapper, IConfiguration configuration) : IBasketService
    {
      
        public async Task<CustomerBasketDTO?> GetCustomerBasketAsync(string id)
        {
           var basket = await basketRepository.GetAsync(id);
           return basket == null ? throw new NotFoundException(nameof(CustomerBasket), id) : mapper.Map<CustomerBasketDTO>(basket);
        }

        public async Task<CustomerBasketDTO?> UpdateCustomerBasketAsync(CustomerBasketDTO customerBasketDTO)
        {
            var mappedBasket = mapper.Map<CustomerBasket>(customerBasketDTO);

            var timeToLive = TimeSpan.FromDays(double.Parse(configuration.GetSection("RedisSettings")["TimeToLiveInDays"]!));

            var updatedBasket = await basketRepository.UpdateAsync(mappedBasket, timeToLive);

            return updatedBasket == null ? throw new BadRequestException("Can't update, there is a problem with your basket") : customerBasketDTO;

        }

        public async Task<bool> DeleteCustomerBasketAsync(string id)
        {
            var deleted =  await basketRepository.DeleteAsync(id);

            if (!deleted) throw new BadRequestException("Unable to delete this basket");
            return deleted;

        }
    }
}
