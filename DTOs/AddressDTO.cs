using System.ComponentModel.DataAnnotations;

namespace E_Commerce.DTOs
{
    public class AddressDTO
    {
        [Required(ErrorMessage = "ZipCode is required")]
        [Range(1, 99999999, ErrorMessage = "Invalid ZipCode format.")]
        public int ZipCode { get; set; }

        [Required(ErrorMessage = "Street is required")]
        [MaxLength(60, ErrorMessage = "Street max length: 60")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Number is required")]
        [MaxLength(20, ErrorMessage = "Number max length: 20")]
        public string Number { get; set; }

        [MaxLength(40, ErrorMessage = "AddressLine2 max length: 40")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "District is required")]
        [MaxLength(40, ErrorMessage = "District max length: 40")]
        public string District { get; set; }

        [Required(ErrorMessage = "State is required")]
        [MaxLength(40, ErrorMessage = "State max length: 40")]
        public string State { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(40, ErrorMessage = "City max length: 40")]
        public string City { get; set; }
    }
}
