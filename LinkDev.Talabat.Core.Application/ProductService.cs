using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Products;

namespace LinkDev.Talabat.Core.Application
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductToReturnDTO>> GetProductsAsync()
        {
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync();

            var productsToReturn = mapper.Map<IEnumerable<ProductToReturnDTO>>(products);
            return productsToReturn;
        }


		public async Task<ProductToReturnDTO> GetProductAsync(int id)
        {
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(id);

            var productToReturn = mapper.Map<ProductToReturnDTO>(product);

            return productToReturn;
        }


        public async Task<IEnumerable<BrandsDTO>> GetBrandsSync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

            var brandsToReturn = mapper.Map<IEnumerable<BrandsDTO>>(brands);

            return brandsToReturn;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesSync()
        {
            var categories = await unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();

            var categoriesToReturn = mapper.Map<IEnumerable<CategoryDTO>>(categories);

            return categoriesToReturn;
        }

    }
}
