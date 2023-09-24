namespace TravelWarrants.DTOs.Searches
{
    public class SearchDTOGet
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public int ClientId { get; set; }


        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
    }
}