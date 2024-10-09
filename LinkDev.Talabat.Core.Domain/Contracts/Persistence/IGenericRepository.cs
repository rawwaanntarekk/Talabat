using LinkDev.Talabat.Core.Domain.Contracts.Specifications;

namespace LinkDev.Talabat.Core.Domain.Contracts.Persistence
{
    public interface IGenericRepository<TEntity, TKey>
                     where TEntity : BaseEntity<TKey>
                     where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false);
        Task<IEnumerable<TEntity>> GetAllAsyncWithSpec(ISpecifications<TEntity , TKey> specification, bool withTracking = false);
        Task<TEntity?> GetAsync(TKey id);
        Task<TEntity?> GetAsyncWithSpec(ISpecifications<TEntity , TKey> specification);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
