using System.Linq.Expressions;

namespace LinkDev.Talabat.Core.Domain.Contracts.Specifications
{
	public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public Expression<Func<TEntity, bool>>? Criteria { get; set; }

		public List<Expression<Func<TEntity, object>>> Includes { get; set; }

        public Expression<Func<TEntity, object>>? OrderBy { get; set; }

        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; }
		public int Skip { get; set; }
		public int Take { get; set; }
		public bool IsPaginationEnabled { get; set; }

		public BaseSpecifications(Expression<Func<TEntity, bool>> criteriaExpression)
        {
			Criteria = criteriaExpression;

			Includes = [];
		}

		public BaseSpecifications(TKey id)
		{
			Criteria = e => e.Id.Equals(id);
			Includes = [];
		}

		private protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
		{
			OrderBy = orderByExpression;
		}
		
		private protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderByDescExpression)
		{
			OrderByDesc = orderByDescExpression;
		}
		private protected void ApplyPagination(int skip, int take)
		{
			IsPaginationEnabled = true;
			Skip = skip;
			Take = take;
		}

		public virtual void AddIncludes() { }
    }
}
