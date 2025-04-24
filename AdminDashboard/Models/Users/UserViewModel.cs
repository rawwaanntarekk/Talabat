using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Models.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
