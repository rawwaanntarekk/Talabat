using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Products;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
	public class StoreContext(DbContextOptions<StoreContext> _options) : DbContext(_options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly);
		}

		public DbSet<ProductBrand> Brands { get; set; }
		public DbSet<ProductCategory> Categories { get; set; }
		public DbSet<Product> Products { get; set; }

		


	}
}
