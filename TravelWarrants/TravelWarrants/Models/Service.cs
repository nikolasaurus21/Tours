namespace TravelWarrants.Models
{
    public class Service
    {
        

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal VATRate { get; set; }
        public virtual ICollection<InoviceService> InoviceService { get; set; }
        public virtual ICollection<Inovice> Inovice { get; set; }
        public virtual ICollection<Inovice> Inovice1 { get; set; }
        public virtual ICollection<Inovice> Inovice2 { get; set; }
        public virtual ICollection<Inovice> Inovice3 { get; set; }
        public virtual ICollection<Inovice> Inovice4 { get; set; }
    }
}
