using System.ComponentModel.DataAnnotations;

namespace PojisteniWebApp.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Jméno")]
        public string FirstName { get; set; } = "";
        [Required]
        [Display(Name = "Přijmení")]
        public string LastName { get; set; } = "";
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";
        [Required]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; } = "";
        [Required]
        [Display(Name = "Ulice")]
        public string Street { get; set; } = "";
        [Required]
        [Display(Name = "Město")]
        public string City { get; set; } = "";
        [Required]
        [Display(Name = "PSČ")]
        public string Zip { get; set; } = "";
        public string FullName => $"{FirstName} {LastName}";
        public ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();

    }
}
