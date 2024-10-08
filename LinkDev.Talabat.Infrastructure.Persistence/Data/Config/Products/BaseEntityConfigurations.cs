using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Products
{
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
