using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config
{
	public class BaseAuditEntityConfigurations<TEntity, Tkey> : BaseEntityConfigurations<TEntity, Tkey> , IEntityTypeConfiguration<TEntity>
             where TEntity : BaseAuditEntity<Tkey>   where Tkey : IEquatable<Tkey>
    {

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.CreatedOn)
                    .IsRequired();

            builder.Property(e => e.CreatedBy)
                   .IsRequired();

            builder.Property(e => e.LastModifiedOn)
                   .IsRequired();

            builder.Property(e => e.LastModifiedBy)
                   .IsRequired();
        }
    }
}
