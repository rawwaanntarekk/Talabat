using LinkDev.Talabat.Core.Domain.Products;

namespace LinkDev.Talabat.Core.Domain.Contracts.Specifications
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
	{
		// The Object Created Via this Constructor will be used to get All Products With Its Brand and Category
        public ProductWithBrandAndCategorySpecifications(string? sort, int? brandId, int? categoryId, int pageSize, int pageIndex) 
			   
			: base(

					 P =>
					 ((!brandId.HasValue) || (brandId == P.BrandId))
					  &&
					 (!categoryId.HasValue || (categoryId == P.CategoryId))
					 
					 
					 )
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

			// totalProducts = 18 
			// pageSize      = 5
			// pageIndex     = 3

			ApplyPagination(pageSize * (pageIndex - 1) , pageSize);
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
