using System.Text.Json.Serialization;

namespace TravelWarrants.Models
{
    public class Client
    {
        public Client()
        {

            this.Account = new HashSet<Account>();
            this.Inovice = new HashSet<Inovice>();
            this.Tour = new HashSet<Tour>();
            this.Payment = new HashSet<Payment>();
            this.Status = new HashSet<Status>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PlaceName { get; set; }
        public string RegistrationNUmber { get; set; }
        public decimal VAT { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string? Note { get; set; }
        public bool? Excursion { get; set; }
        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Inovice> Inovice { get; set; }
        public virtual ICollection<Tour> Tour { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<Status> Status { get; set; }
    }
}
