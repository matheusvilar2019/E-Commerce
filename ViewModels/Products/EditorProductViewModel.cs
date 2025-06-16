using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModels.Products
{
    public class EditorProductViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 80 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(600, MinimumLength = 3, ErrorMessage = "Description must be between 3 and 600 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Slug is required")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Slug must be between 3 and 80 characters")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string Image { get; set; }
    }
}
