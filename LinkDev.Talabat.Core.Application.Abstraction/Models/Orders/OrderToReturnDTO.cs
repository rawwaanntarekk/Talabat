using LinkDev.Talabat.Core.Application.Abstraction.Models._Common;

namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Orders
{
    public class OrderToReturnDTO
    {
        public int Id { get; set; }
        public required string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public required string Status { get; set; } 
        public required AddressDTO ShippingAddress { get; set; }
        public int? DeliveryMethodId { get; set; }
        public  string? DeliveryMethod { get; set; }
        public virtual required ICollection<OrderItemDTO> Items { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

    }
}
