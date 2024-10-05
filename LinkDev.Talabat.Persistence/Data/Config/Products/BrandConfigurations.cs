using LinkDev.Talabat.Core.Domain.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Products
{
    internal class BrandConfigurations : BaseEntityConfigurations<ProductBrand, int>
    {
        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.Name)
                   .IsRequired();


        }
    }
}
