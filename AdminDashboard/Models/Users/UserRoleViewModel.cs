using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Models.Users
{
    public class UserRoleViewModel
    {
        [Display(Name = "User Id")]
        public string UserId { get; set; }
        public string Username { get; set; }
        public List<UpdateRoleViewModel> Roles { get; set; }
    }
}
