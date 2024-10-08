using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories;
using System.Collections.Concurrent;

namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
    internal class UnitOfWork(StoreContext dbContext) : IUnitOfWork
    {
        private readonly ConcurrentDictionary<string, object> _repositories = new();

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseAuditEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return (IGenericRepository<TEntity, TKey>) _repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(dbContext));
        }


        public async Task<int> CompleteAsync()
        => await dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        => await dbContext.DisposeAsync();

    }
}
