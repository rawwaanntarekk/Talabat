using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services
{
	public interface IProductService
	{
		Task<IEnumerable<ProductToReturnDTO>> GetProductsAsync(ProductSpecParams specParams);
		Task<ProductToReturnDTO> GetProductAsync(int id);
		Task<IEnumerable<BrandsDTO>> GetBrandsAsync();
		Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
	}
}
