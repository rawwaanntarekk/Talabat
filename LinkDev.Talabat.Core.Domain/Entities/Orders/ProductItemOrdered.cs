namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
    // This will be owned entity by order item entity
    public class ProductItemOrdered
    {
        public int ProductItemId { get; set; }
        public required string ProductName { get; set; }
        public required string PictureUrl { get; set; }
    }
}
