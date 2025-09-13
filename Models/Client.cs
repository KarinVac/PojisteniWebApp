using System.Collections.Generic;

namespace PojisteniWebApp.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Street { get; set; } = "";
        public string City { get; set; } = "";
        public string Zip { get; set; } = "";
        public string FullName => $"{FirstName} {LastName}";
        public ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();
    }
}

