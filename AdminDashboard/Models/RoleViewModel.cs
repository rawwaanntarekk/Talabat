using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Role Name Is Required !")]
        [StringLength(100)]

        public string Name { get; set; }
    }
}
