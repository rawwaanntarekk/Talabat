using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Products;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
    class StoreContextInitializer(StoreContext _dbContext) : IStoreContextInitializer
    {
        public async Task InitializeAsync()
        {
           var  pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await _dbContext.Database.MigrateAsync();
            }
        }

        public async Task SeedAsync()
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
