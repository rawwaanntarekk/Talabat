using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Basket
{
    public class BasketController(IServiceManager serviceManager) : BaseAPIController
    {
        [HttpGet] // GET: /api/Basket?id=

        public async Task<ActionResult> GetBasket( string id)
        {
            var basket = await serviceManager.BasketService.GetCustomerBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost] // POST: /api/Basket
        public async Task<ActionResult<CustomerBasketDTO>> UpdateBasket(CustomerBasketDTO basketDTO)
        {
            var basket = await serviceManager.BasketService.UpdateCustomerBasketAsync(basketDTO);
            return Ok(basket);
        }

        [HttpDelete] // DELETE: /api/Basket?id=
        public async Task DeleteBasket(string id)
        {
            await serviceManager.BasketService.DeleteCustomerBasketAsync(id);
            
        }

    }
}
