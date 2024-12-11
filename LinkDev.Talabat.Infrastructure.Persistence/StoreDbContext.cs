using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Common;
using System.Reflection;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
	public class StoreDbContext(DbContextOptions<StoreDbContext> _options) : DbContext(_options)
	{
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly,
			type => type.GetCustomAttribute<DbContextTypeAttribute>()?.DbContextType == typeof(StoreDbContext));

		}

		public DbSet<ProductBrand> Brands { get; set; }
		public DbSet<ProductCategory> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Order> Orders { get; set; }




    }
}
