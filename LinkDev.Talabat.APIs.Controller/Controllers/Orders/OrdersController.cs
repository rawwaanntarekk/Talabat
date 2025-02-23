using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Orders
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager) : BaseAPIController
    {
        [HttpPost] // POST: api/Orders
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderToCreateDTO orderDTO)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = await _serviceManager.OrderService.CreateOrderAsync( buyerEmail!,orderDTO);

            return Ok(result);
        }
    }
}
