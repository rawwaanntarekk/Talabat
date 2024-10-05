using LinkDev.Talabat.Core.Domain.Common;

namespace LinkDev.Talabat.Core.Domain.Products
{
    public class Product : BaseEntity<int>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int? BrandId { get; set; } // Forign key 
        public virtual ProductBrand? Brand { get; set; }
        public int? MyProperty { get; set; } // Forign key
        public virtual ProductCategory? Category { get; set; }





    }
}
