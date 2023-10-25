namespace TravelWarrants.DTOs
{
    public class ResponseDTO<T>
    {

        public bool IsSucced { get; set; }
        public T Message { get; set; }
        public int? TotalPages { get; set; }

        public string? ErrorMessage { get; set; }

    }
}
