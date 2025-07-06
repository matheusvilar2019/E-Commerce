using E_Commerce.DTOs;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModels
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-mail is required")]
        [EmailAddress(ErrorMessage = "E-mail is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "CPF is required")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Birthdate is required")]
        public DateTime BirthDate { get; set; }

        public AddressDTO Address { get; set; }
    }
}
