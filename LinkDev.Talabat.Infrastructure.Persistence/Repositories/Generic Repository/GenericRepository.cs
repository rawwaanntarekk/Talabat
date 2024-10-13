using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Contracts.Specifications;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories.Specifications;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
	internal class GenericRepository<TEntity, TKey>(StoreContext _dbcontext) : IGenericRepository<TEntity, TKey>
                     where TEntity : BaseEntity<TKey>
                     where TKey : IEquatable<TKey>
    {
        // This implementation violates the Open/Closed Principle 
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false) 
        {
           return withTracking?
              await _dbcontext.Set<TEntity>().ToListAsync()
            : await _dbcontext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

		public async  Task<IEnumerable<TEntity>> GetAllAsyncWithSpec(ISpecifications<TEntity, TKey> specification, bool withTracking = false)
		{
			return await ApplySpecification(specification).ToListAsync();
		}

        public async Task<TEntity?> GetAsync(TKey id)
        {
           return await _dbcontext.Set<TEntity>().FindAsync(id);
		}

		public async Task<TEntity?> GetAsyncWithSpec(ISpecifications<TEntity, TKey> specification)
		{
		    return await ApplySpecification(specification).FirstOrDefaultAsync();
		}
 
        public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> specification)
		{
			return await ApplySpecification(specification).CountAsync();
		}
        public async  Task AddAsync(TEntity entity)
        => await _dbcontext.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity)
        => _dbcontext.Set<TEntity>().Remove(entity);

        public void Delete(TEntity entity)
        => _dbcontext.Set<TEntity>().Update(entity);

        #region Helpers

        private IQueryable<TEntity> ApplySpecification(ISpecifications<TEntity, TKey> specification)
        =>  SpecificationEvaluator<TEntity, TKey>.GetQuery(_dbcontext.Set<TEntity>(), specification);
		
        #endregion

    }
}
