using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
	public class ProductsController(IServiceManager serviceManager) : BaseAPIController
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDTO>>> GetProducts()
		{
			var products = await serviceManager.ProductService.GetProductsAsync();
			return Ok(products);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)
		{
			var product = await serviceManager.ProductService.GetProductAsync(id);

			if(product is null)
				return NotFound();

			return Ok(product);
		}

		[HttpGet("brands")] // GET /api/products/brands
		public async Task<ActionResult<IEnumerable<BrandsDTO>>> GetBrands()
		{
			var brands = await serviceManager.ProductService.GetBrandsAsync();
			return Ok(brands);
		}

		[HttpGet("categories")] // GET /api/products/categories
		public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
		{
			var categories = await serviceManager.ProductService.GetCategoriesAsync();
			return Ok(categories);
		}


	}
}
