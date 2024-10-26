using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Infrastructure.Persistence.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Products
{
	[DbContextType(typeof(StoreDbContext))]
	public class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
		   where TEntity : BaseEntity<TKey>  where TKey : IEquatable<TKey> 
	{
		public virtual void Configure(EntityTypeBuilder<TEntity> builder)
		{

			builder.Property(e => e.Id)
				   .ValueGeneratedOnAdd();
		}
	}

}
