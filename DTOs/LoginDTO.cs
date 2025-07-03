using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModels
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Informe o E-mail")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe a senha")]
        public string Password { get; set; }
    }
}
