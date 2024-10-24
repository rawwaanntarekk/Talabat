using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Core.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual required string DisplayName { get; set; }
        public virtual Address? Address { get; set; }
    }
}
