using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config
{
    public class BaseEntityConfigurations<TEntity, Tkey> : IEntityTypeConfiguration<TEntity>
             where TEntity : BaseEntity<Tkey>   where Tkey : IEquatable<Tkey>
    {

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

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
