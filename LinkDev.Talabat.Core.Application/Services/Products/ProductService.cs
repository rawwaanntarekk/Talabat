using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Contracts.Specifications;
using LinkDev.Talabat.Core.Domain.Products;

namespace LinkDev.Talabat.Core.Application.Services.Products
{
	internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductToReturnDTO>> GetProductsAsync(ProductSpecParams specParams)
        {
            var specification = new ProductWithBrandAndCategorySpecifications(specParams.Sort, specParams.BrandId, specParams.CategoryId, specParams.PageSize, specParams.PageIndex);

            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsyncWithSpec(specification);

            var productsToReturn = mapper.Map<IEnumerable<ProductToReturnDTO>>(products);
            return productsToReturn;
        }


        public async Task<ProductToReturnDTO> GetProductAsync(int id)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(id);

            var productToReturn = mapper.Map<ProductToReturnDTO>(product);

            return productToReturn;
        }


        public async Task<IEnumerable<BrandsDTO>> GetBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

            var brandsToReturn = mapper.Map<IEnumerable<BrandsDTO>>(brands);

            return brandsToReturn;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            var categories = await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();

            var categoriesToReturn = mapper.Map<IEnumerable<CategoryDTO>>(categories);

            return categoriesToReturn;
        }

		
	}
}
