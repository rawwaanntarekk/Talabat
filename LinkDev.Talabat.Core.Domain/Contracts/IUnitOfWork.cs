using LinkDev.Talabat.Core.Domain.Products;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable 
    {
        IGenericRepository<TEntity, TKey> GetRepository<TEntity,TKey>()
            where TEntity : BaseAuditEntity<TKey> where TKey : IEquatable<TKey>;
        Task<int> CompleteAsync();
    }
}
