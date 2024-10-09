using LinkDev.Talabat.Core.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts.Specifications
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
	{
		// The Object Created Via this Constructor will be used to get All Products With Its Brand and Category
        public ProductWithBrandAndCategorySpecifications() : base()
        {
            Includes.Add(p => p.Brand!);
            Includes.Add(p => p.Category!);
        }

        public ProductWithBrandAndCategorySpecifications(int id) : base(id)
		{
			Includes.Add(p => p.Brand!);
			Includes.Add(p => p.Category!);
		}

    }
	
}
