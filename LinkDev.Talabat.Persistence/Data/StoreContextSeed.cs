using LinkDev.Talabat.Core.Domain.Products;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbcontext)
        {
            // Brands Seeding
            if (!dbcontext.Brands.Any())
            {
                var brandsData = await File.ReadAllTextAsync($"../LinkDev.Talabat.Persistence/Data/Seeds/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count > 0)
                {
                    await dbcontext.Brands.AddRangeAsync(brands);
                    await dbcontext.SaveChangesAsync();
                }
            }

            // Categories Seeding
            if (!dbcontext.Categories.Any())
            {
                var CategoriesData = await File.ReadAllTextAsync("../LinkDev.Talabat.Persistence/Data/Seeds/categories.json");
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);

                if(Categories?.Count > 0)
                {
                    await dbcontext.Categories.AddRangeAsync(Categories);
                    await dbcontext.SaveChangesAsync();
                }



            }

            // Products Seeding
            if (!dbcontext.Products.Any())
            {
                var ProductsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Persistence/Data/Seeds/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (Products?.Count > 0)
                {
                    await dbcontext.Products.AddRangeAsync(Products);
                    await dbcontext.SaveChangesAsync();
                }
            }
        }
    }
}
