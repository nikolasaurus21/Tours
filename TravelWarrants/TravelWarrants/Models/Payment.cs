using System.ComponentModel.DataAnnotations;

namespace TravelWarrants.Models
{
    public class Payment
    { 

        public int Id { get; set; }
        public int ClientId { get; set; }

        
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public virtual Client Client { get; set; }

    }
}
