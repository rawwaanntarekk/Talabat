using LinkDev.Talabat.Core.Domain.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public class StoreDbContext(DbContextOptions<StoreDbContext> _options, SaveChangesInterceptor _saveChangesInterceptor) : DbContext(_options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly,
			type => type.GetCustomAttribute<DbContextTypeAttribute>()?.DbContextType == typeof(StoreDbContext));

		}
      

        public DbSet<ProductBrand> Brands { get; set; }
		public DbSet<ProductCategory> Categories { get; set; }
		public DbSet<Product> Products { get; set; }

		


	}
}
