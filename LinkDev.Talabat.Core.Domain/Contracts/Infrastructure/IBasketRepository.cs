using LinkDev.Talabat.Core.Domain.Basket;
namespace LinkDev.Talabat.Core.Domain.Contracts.Infrastructure
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetAsync(string id);
        Task<CustomerBasket?> UpdateAsync(CustomerBasket customerBasket, TimeSpan timeToLive);
        Task<bool> DeleteAsync(string id);
    }

}
