using LinkDev.Talabat.Core.Domain.Products;
using System.ComponentModel.DataAnnotations;

namespace Talabat.Dashboard.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public IFormFile? Image   { get; set; }
        public string? PictureURL { get; set; }

        [Range(1,1000)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category Id is required.")]
        public int? CategoryId { get; set; }
        public ProductCategory? Category { get; set; }

        [Required(ErrorMessage = "Brand Id is required.")]
        public int? BrandId { get; set; }
        public ProductBrand? Brand { get; set; }






    }
}
