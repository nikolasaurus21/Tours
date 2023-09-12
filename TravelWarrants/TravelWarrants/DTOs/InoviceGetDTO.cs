using System.Diagnostics;

namespace TravelWarrants.DTOs
{
    public class InoviceGetDTO
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Year { get; set; }
        public string ClientName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date {  get; set; }
    }
}
