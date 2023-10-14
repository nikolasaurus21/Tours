namespace TravelWarrants.Models
{
    public class UploadedFiles
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public ProformaInvoice ProformaInvoice { get; set; }
    }
}
