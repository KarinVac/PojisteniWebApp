using System.ComponentModel.DataAnnotations;

namespace PojisteniWebApp.Models
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vyplňte jméno")]
        [Display(Name = "Jméno")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte příjmení")]
        [Display(Name = "Příjmení")]
        public string LastName { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte e-mail")]
        [EmailAddress(ErrorMessage = "Neplatný formát e-mailu")]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte telefon")]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte ulici")]
        [Display(Name = "Ulice")]
        public string Street { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte město")]
        [Display(Name = "Město")]
        public string City { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte PSČ")]
        [Display(Name = "PSČ")]
        public string Zip { get; set; } = "";

        public string FullName => $"{FirstName} {LastName}";
        public ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();
    }
}

