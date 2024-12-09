using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Config;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkDev.Talabat.Infrastructure.Persistence._Data.Config.Orders
{
    public class OrderConfigurations : BaseAuditEntityConfigurations<Order, int>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(O => O.ShippingAddress)
                   .WithOwner();

            builder.Property(O => O.Status)
                   .HasConversion
                   (
                     (OStatus) => OStatus.ToString(),
                     (OStatus) => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)
                   );

            builder.Property(O => O.Subtotal)
                   .HasColumnType("decimal(8,2)");

            builder.HasOne(O => O.DeliveryMethod)
                   .WithMany()
                   .HasForeignKey(O => O.DeliveryMethodId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(O => O.Items)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);
                  
        }
    }
}
