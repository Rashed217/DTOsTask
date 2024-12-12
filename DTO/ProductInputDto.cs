using System.ComponentModel.DataAnnotations;

namespace DTOsTask.DTO
{
    public class ProductInputDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        public string Category { get; set; } = "general"; // Default to "general"
    }
}
