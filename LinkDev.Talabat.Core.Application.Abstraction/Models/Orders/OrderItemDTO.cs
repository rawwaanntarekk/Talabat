namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Orders
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int ProductItemId { get; set; }
        public required string ProductName { get; set; }
        public required string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
