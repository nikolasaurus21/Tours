namespace TravelWarrants.Models
{
    public class Company
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PTT { get; set; }
        public string Place { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string MobilePhone { get; set; }
        public string TIN { get; set; }
        public string VAT { get; set; }
        public virtual ICollection<CompanyBankAccount> CompanyGiroAccount { get; set; }
    }
}
