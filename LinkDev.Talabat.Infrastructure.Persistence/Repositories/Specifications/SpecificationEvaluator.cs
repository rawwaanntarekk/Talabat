﻿using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

			query = specification.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));

			return query;


		}
	}
}
