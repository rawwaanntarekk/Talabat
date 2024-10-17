using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Basket
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public string? PictureUrl { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
        public string? Brand { get; set; }
        public string? Category { get; set; }
    }
}
