using System.ComponentModel.DataAnnotations;

namespace PojisteniWebApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vyplňte jméno")]
        [Display(Name = "Jméno")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte příjmení")]
        [Display(Name = "Příjmení")]
        public string LastName { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte telefon")]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte ulici")]
        [Display(Name = "Ulice a č.p.")]
        public string Street { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte město")]
        [Display(Name = "Město")]
        public string City { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte PSČ")]
        [Display(Name = "PSČ")]
        public string Zip { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte e-mailovou adresu")]
        [EmailAddress(ErrorMessage = "Neplatná e-mailová adresa")]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte heslo")]
        [StringLength(100, ErrorMessage = "{0} musí mít délku alespoň {2} a nejvíc {1} znaků.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte heslo znovu")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrzení hesla")]
        [Compare(nameof(Password), ErrorMessage = "Zadaná hesla se musí shodovat.")]
        public string ConfirmPassword { get; set; } = "";
    }
}
