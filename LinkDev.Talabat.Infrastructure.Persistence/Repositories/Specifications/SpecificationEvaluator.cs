using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Specifications;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories.Specifications
{
	internal static class SpecificationEvaluator<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specification)
		{

			var query = inputQuery; // _dbContext.Set<TEntity>()

			if(specification.Criteria != null) // p => p.Id.Equals(id)
				query = query.Where(specification.Criteria);
			// query = _dbContext.Products.Where(p => p.Id.Equals(id));

			if (specification.OrderByDesc is not null)
				query = query.OrderByDescending(specification.OrderByDesc);
			else if (specification.OrderBy is not null)
				query = query.OrderBy(specification.OrderBy);

			if (specification.IsPaginationEnabled)
				query = query.Skip(specification.Skip).Take(specification.Take);
				

			query = specification.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

			return query;


		}
	}
}
