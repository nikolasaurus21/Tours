using System.ComponentModel.DataAnnotations.Schema;

namespace TravelWarrants.Models
{
    public class Status
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal AmountOfAccount { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal AmountOfDeposit { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Balance { get; set; }
        public virtual Client Client { get; set; }
    }
}
