using System.ComponentModel.DataAnnotations;

namespace PojisteniWebApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vyplňte e-mailovou adresu")]
        [EmailAddress(ErrorMessage = "Neplatná e-mailová adresa")]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte heslo")]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";

        [Display(Name = "Pamatuj si mě")]
        public bool RememberMe { get; set; }
    }
}
