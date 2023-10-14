namespace TravelWarrants.DTOs
{
    public class FileData
    {
        public byte[] FileBytes { get; set; }
        public string FileName { get; set; }

        public Stream? FileStream { get; set; }
    }
}