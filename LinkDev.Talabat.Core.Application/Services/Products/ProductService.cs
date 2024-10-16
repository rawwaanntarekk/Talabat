using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Common;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Common.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Contracts.Specifications;
using LinkDev.Talabat.Core.Domain.Products;

namespace LinkDev.Talabat.Core.Application.Services.Products
{
	internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<Pagination<ProductToReturnDTO>> GetProductsAsync(ProductSpecParams specParams)
        {
            var specification = new ProductWithBrandAndCategorySpecifications(specParams.Sort, specParams.BrandId, specParams.CategoryId, specParams.PageSize, specParams.PageIndex, specParams.Search);

            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsyncWithSpec(specification);

            var data = mapper.Map<IEnumerable<ProductToReturnDTO>>(products);

            var countSpecification = new ProductWithFilterationForCountSpecifications(specParams.BrandId, specParams.CategoryId, specParams.Search);


            var count = await unitOfWork.GetRepository<Product, int>().GetCountAsync(countSpecification);
            
            return new Pagination<ProductToReturnDTO>(specParams.PageIndex, specParams.PageSize, data, count);
        }


        public async Task<ProductToReturnDTO> GetProductAsync(int id)
        {
            var specification = new ProductWithBrandAndCategorySpecifications(id);

            var product = await unitOfWork.GetRepository<Product, int>().GetAsyncWithSpec(specification);

            if (product is null)
                throw new NotFoundException(nameof(Product), id);

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
