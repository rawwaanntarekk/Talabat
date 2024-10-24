using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Products;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
    class StoreDbInitializer(StoreDbContext _dbContext) : DbInitialzer(_dbContext), IStoreDbInitializer
    {
       

        public override async Task SeedAsync()
        {
            // Brands Seeding
            if (!_dbContext.Brands.Any())
            {
                var brandsData = await File.ReadAllTextAsync($"../LinkDev.Talabat.Persistence/Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count > 0)
                {
                    await _dbContext.Brands.AddRangeAsync(brands);
                    await _dbContext.SaveChangesAsync();
                }
            }

            // Categories Seeding
            if (!_dbContext.Categories.Any())
            {
                var CategoriesData = await File.ReadAllTextAsync("../LinkDev.Talabat.Persistence/Data/Seeds/categories.json");
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);

                if (Categories?.Count > 0)
                {
                    await _dbContext.Categories.AddRangeAsync(Categories);
                    await _dbContext.SaveChangesAsync();
                }



            }

            // Products Seeding
            if (!_dbContext.Products.Any())
            {
                var ProductsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Persistence/Data/Seeds/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (Products?.Count > 0)
                {
                    await _dbContext.Products.AddRangeAsync(Products);
                    await _dbContext.SaveChangesAsync();
                }
            }

        }

       
    }
}
