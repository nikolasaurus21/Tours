namespace TravelWarrants.Models
{
    public class Client
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PlaceName { get; set; }
        public string RegistrationNumber { get; set; }
        public decimal VAT { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string? Note { get; set; }
        public bool? Excursion { get; set; }
        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Invoice> Inovice { get; set; }
        public virtual ICollection<ProformaInvoice> ProformaInvoice { get; set; }
        public virtual ICollection<Tour> Tour { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<Status> Status { get; set; }
    }
}
