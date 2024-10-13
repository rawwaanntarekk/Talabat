using LinkDev.Talabat.Core.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts.Specifications
{
	public class ProductWithFilterationForCountSpecifications : BaseSpecifications<Product, int>

	{
		public ProductWithFilterationForCountSpecifications(int? brandId, int? categoryId)
			: base(
				  p =>
					(!brandId.HasValue || p.BrandId == brandId) 
					&&
					(!categoryId.HasValue || p.CategoryId == categoryId)
				  )
		{
		}
	}
}
