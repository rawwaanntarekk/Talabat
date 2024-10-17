using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Basket
{
    public interface IBasketService
    {
        Task<CustomerBasketDTO?> GetCustomerBasketAsync(string id);
        Task<CustomerBasketDTO?> UpdateCustomerBasketAsync(CustomerBasketDTO customerBasket);
        Task<bool> DeleteCustomerBasketAsync(string id);

    }
}
