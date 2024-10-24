using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Infrastructure.Persistence._Identity
{
    internal class StoreIdentityDbInitialzer(StoreIdentityDbContext _dbContext,
                                            UserManager<ApplicationUser> _userManager) : DbInitialzer(_dbContext), IStoreIdentityDbInitializer
    {
        public override async Task SeedAsync()
        {
            var user = new ApplicationUser
            {
                DisplayName = "Rawan Tarek",
                UserName = "rawanntarekk",
                Email = "rawan@gmail.com",
                PhoneNumber = "01154155524"
            };

            await _userManager.CreateAsync(user, "P@ssw@rd");

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
