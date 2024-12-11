using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Orders
{
    public interface IOrderService
    {
        Task<OrderToReturnDTO> CreateOrderAsync(string buyerEmail, OrderToCreateDTO order);
        Task<OrderToReturnDTO> GetOrderByIdAsync (string buyerEmail, int orderId);
        Task<IEnumerable<OrderToReturnDTO>> GetOrderForUserAsync (string buyerEmail);
        Task<IEnumerable<DeliveryMethodDTO>> GetDeliveryMethodsAsync();

    }
}
