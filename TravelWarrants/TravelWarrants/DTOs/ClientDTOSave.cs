namespace TravelWarrants.DTOs
{
    public class ClientDTOSave
    {
        
        public string Name { get; set; }
        public string Address { get; set; }
        public string PlaceName { get; set; }
        public string RegistrationNumber {get; set;}
        public decimal VAT { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool? Excursion { get; set; }
    }
}
