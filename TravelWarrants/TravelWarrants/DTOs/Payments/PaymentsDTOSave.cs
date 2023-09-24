using System.ComponentModel.DataAnnotations;

namespace TravelWarrants.DTOs.Payments
{
    public class PaymentsDTOSave
    {

        public int ClientId { get; set; }
        // public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
    }
}
