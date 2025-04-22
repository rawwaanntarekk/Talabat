namespace LinkDev.Talabat.Core.Domain.Products
{
    public class ProductCategory : BaseAuditEntity<int>
    {
        public required string Name { get; set; }
    }
}
