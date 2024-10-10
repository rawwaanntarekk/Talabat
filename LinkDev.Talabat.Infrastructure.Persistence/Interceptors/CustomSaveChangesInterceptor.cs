using LinkDev.Talabat.Core.Application.Abstraction.Contracts;
using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LinkDev.Talabat.Infrastructure.Persistence.Interceptors
{
	internal class CustomSaveChangesInterceptor(ILoggedInUserService loggedInUserService) : SaveChangesInterceptor
	{

		public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
		{
			UpdateEntites(eventData.Context);
			return base.SavingChanges(eventData, result);
		}

		public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData,
														int result, CancellationToken cancellationToken = default)
		{
			UpdateEntites(eventData.Context);
			return base.SavedChangesAsync(eventData, result, cancellationToken);
		}

		private  void UpdateEntites(DbContext? context)
		{
			if (context is not null)
			{ 
				//                                                  The key should be a generic type.
				foreach (var entry in context.ChangeTracker.Entries<BaseAuditEntity<int>>()
					.Where(entity => entity.State is EntityState.Added or EntityState.Modified))
				{
					if (entry.State == EntityState.Added)
					{
						entry.Entity.CreatedBy = loggedInUserService.UserId!;
						entry.Entity.CreatedOn = DateTime.Now;
					}

					entry.Entity.LastModifiedOn = DateTime.UtcNow;
					entry.Entity.LastModifiedBy = loggedInUserService.UserId!;

				}

			}
		}
	}
}
