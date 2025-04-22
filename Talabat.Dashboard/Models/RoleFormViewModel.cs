using System.ComponentModel.DataAnnotations;

namespace Talabat.Dashboard.Models
{
    public class RoleFormViewModel
    {
        [Required(ErrorMessage = "role Name is required")]
        [StringLength(256)]
        public string Name { get; set; } = null!;
    }
}
