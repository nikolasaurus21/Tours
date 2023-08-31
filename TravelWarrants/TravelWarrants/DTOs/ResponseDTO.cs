using System.Text.Json.Serialization;

namespace TravelWarrants.DTOs
{
    public class ResponseDTO<T>
    {
        
        public bool IsSucced { get; set; }
        public T Message { get; set; }
        
    }
}
