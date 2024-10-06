using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories;

namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private readonly Lazy<IGenericRepository<ProductBrand, int>> _brandsRepository;
        private readonly Lazy<IGenericRepository<ProductCategory, int>> _categoriesRepository;
        private readonly Lazy<IGenericRepository<Product, int>> _productsRepository;
        

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _brandsRepository = new Lazy<IGenericRepository<ProductBrand, int>>(() => new GenericRepository<ProductBrand, int>(_dbContext));
            _categoriesRepository = new Lazy<IGenericRepository<ProductCategory, int>>(() => new GenericRepository<ProductCategory, int>(_dbContext));
            _productsRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenericRepository<Product, int>(_dbContext));
            
        }

        public IGenericRepository<ProductBrand, int> brandsRepository => _brandsRepository.Value;
        public IGenericRepository<ProductCategory, int> categoriesRepository => _categoriesRepository.Value;
        public IGenericRepository<Product, int> productsRepository => _productsRepository.Value;

        public Task<int> CompleteAsync()
        => _dbContext.SaveChangesAsync();

        public ValueTask DisposeAsync()
        => _dbContext.DisposeAsync();
    }
}
