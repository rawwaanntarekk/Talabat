using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Concurrent;

namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
    internal class UnitOfWork(StoreDbContext dbContext,
                              SaveChangesInterceptor _interceptor  ) : IUnitOfWork
    {
        private readonly ConcurrentDictionary<string, object> _repositories = new();

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return (IGenericRepository<TEntity, TKey>) _repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(dbContext));
        }


        public async Task<int> CompleteAsync()
        {
            return await dbContext.SaveChangesAsync();

        }

        public async ValueTask DisposeAsync()
        => await dbContext.DisposeAsync();

    }
}
