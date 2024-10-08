using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoreContext _dbcontext) : IGenericRepository<TEntity, TKey>
                     where TEntity : BaseAuditEntity<TKey>
                     where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
        => withTracking ? await _dbcontext.Set<TEntity>().ToListAsync()
                        : await _dbcontext.Set<TEntity>().AsNoTracking().ToListAsync();
        

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
