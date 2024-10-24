﻿namespace LinkDev.Talabat.Core.Domain.Products
{
    public class Product : BaseAuditEntity<int>
    {
        public required string Name { get; set; }
        public required string NormalizedName { get; set; }
        public required string Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int? BrandId { get; set; } // Forign key 
        public virtual ProductBrand? Brand { get; set; }
        public int? CategoryId { get; set; } // Forign key
        public virtual ProductCategory? Category { get; set; }





    }
}
