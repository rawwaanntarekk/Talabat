using System.ComponentModel.DataAnnotations;

namespace Talabat.Dashboard.Models
{
    public class ProductBrandViewModel
    {
        [Required(ErrorMessage = "Brand name is required")]
        public string Name { get; set; }
    }
}
