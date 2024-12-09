namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
    public class OrderItem : BaseAuditEntity<int>
    {
        public required ProductItemOrdered ItemOrdered { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
