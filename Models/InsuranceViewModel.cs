using System;
using System.ComponentModel.DataAnnotations;

namespace PojisteniWebApp.Models
{
    public class InsuranceViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vyplňte předmět pojištění")]
        [Display(Name = "Předmět pojištění")]
        public string Type { get; set; } = "";

        [Required(ErrorMessage = "Vyplňte částku")]
        [Display(Name = "Částka")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Vyplňte datum začátku platnosti")]
        [Display(Name = "Platnost od:")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Vyplňte datum konce platnosti")]
        [Display(Name = "Platnost do:")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Musíte vybrat pojištěnce")]
        [Display(Name = "Pojištěnec")]
        public int ClientId { get; set; }
                
        public ClientViewModel? Client { get; set; }
    }
}

