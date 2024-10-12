using LinkDev.Talabat.Core.Domain.Products;

namespace LinkDev.Talabat.Core.Domain.Contracts.Specifications
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
	{
		// The Object Created Via this Constructor will be used to get All Products With Its Brand and Category
        public ProductWithBrandAndCategorySpecifications(string? sort) 
			   : base()
		{
			AddIncludes();

			AddOrderBy(P => P.Name);

			if (!string.IsNullOrEmpty(sort))
			{
				switch (sort)
				{
					case "nameDesc":
						AddOrderByDesc(P => P.Name);
						break;
					case "priceAsc":
						AddOrderBy(P => P.Price);
						break;
					case "priceDesc":
						AddOrderByDesc(P => P.Price);
						break;
					default:
						break;
				}
			}
		}


		// The Object Created Via this Constructor will be used to get a Specific Product With Its Brand and Category

		public ProductWithBrandAndCategorySpecifications(int id) : base(id)
		{
			AddIncludes();
		}

		private void AddIncludes()
		{
			Includes.Add(p => p.Brand!);
			Includes.Add(p => p.Category!);
		}
    }
	
}
