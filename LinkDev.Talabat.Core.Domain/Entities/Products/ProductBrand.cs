namespace LinkDev.Talabat.Core.Domain.Products
{
	public class ProductBrand : BaseAuditEntity<int>
    {
        public required string Name { get; set; }
    }
}
