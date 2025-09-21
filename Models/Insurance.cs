using System.ComponentModel.DataAnnotations;

namespace PojisteniWebApp.Models
{
    public class Insurance
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Předmět pojištění")]
        public string Type { get; set; } = "";

        [Required]
        [Display(Name = "Částka")]
        public int Amount { get; set; }

        [Required]
        [Display(Name = "Platnost od:")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Platnost do:")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }


        [Required]
        [Display(Name = "Pojištěnec")]
        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
