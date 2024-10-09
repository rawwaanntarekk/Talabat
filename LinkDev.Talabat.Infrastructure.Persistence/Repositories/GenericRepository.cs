using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Products;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoreContext _dbcontext) : IGenericRepository<TEntity, TKey>
                     where TEntity : BaseEntity<TKey>
                     where TKey : IEquatable<TKey>
    {
        // This implementation violates the Open/Closed Principle 
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false) {
            if (typeof(TEntity) == typeof(Product))
                return withTracking? 
                       (IEnumerable<TEntity>) await _dbcontext.Products.Include(p => p.Brand).Include(p => p.Category).ToListAsync() :
                       (IEnumerable<TEntity>) await _dbcontext.Products.AsNoTracking().Include(p => p.Brand).Include(p => p.Category).ToListAsync();

           return withTracking?
              await _dbcontext.Set<TEntity>().ToListAsync()
            : await _dbcontext.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        

        public async Task<TEntity?> GetAsync(TKey id)
           => await _dbcontext.Set<TEntity>().FindAsync(id);


        public async  Task AddAsync(TEntity entity)
        => await _dbcontext.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity)
        => _dbcontext.Set<TEntity>().Remove(entity);

        public void Delete(TEntity entity)
        => _dbcontext.Set<TEntity>().Update(entity);


    }
}
