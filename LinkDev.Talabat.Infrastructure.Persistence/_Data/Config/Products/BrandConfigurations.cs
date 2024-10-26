using LinkDev.Talabat.Core.Domain.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Products
{
	internal class BrandConfigurations : BaseAuditEntityConfigurations<ProductBrand, int>
    {
        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.Name)
                   .IsRequired();

            builder.HasIndex(b => b.Name);

        }
    }
}
