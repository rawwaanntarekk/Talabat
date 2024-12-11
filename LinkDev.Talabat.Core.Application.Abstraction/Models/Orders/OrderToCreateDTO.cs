using LinkDev.Talabat.Core.Application.Abstraction.Models._Common;

namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Orders
{
    public class OrderToCreateDTO
    {
        public required string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public required AddressDTO ShippingAddress { get; set; }

    }
}
