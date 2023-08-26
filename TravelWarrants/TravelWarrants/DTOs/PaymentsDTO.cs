using System.ComponentModel.DataAnnotations;

namespace TravelWarrants.DTOs
{
    public class PaymentsDTO
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public int ClientId { get; set; }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
    }
}
