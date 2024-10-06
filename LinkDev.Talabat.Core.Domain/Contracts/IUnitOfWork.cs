using LinkDev.Talabat.Core.Domain.Products;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IGenericRepository<ProductBrand , int > brandsRepository { get; }
        public IGenericRepository<ProductCategory , int > categoriesRepository  { get; }
        public IGenericRepository<Product, int>  productsRepository { get;  }
        Task<int> CompleteAsync();
    }
}
