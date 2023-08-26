namespace TravelWarrants.Models
{
    public class Status
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public decimal AmountOfAccount { get; set; }
        public decimal AmountOfDeposit { get; set; }
        public decimal Balance { get; set; }
        public virtual Client Client { get; set; }
    }
}
