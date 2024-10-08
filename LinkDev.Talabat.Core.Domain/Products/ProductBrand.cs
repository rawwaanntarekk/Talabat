using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Products
{
    public class ProductBrand : BaseAuditEntity<int>
    {
        public required string Name { get; set; }
    }
}
