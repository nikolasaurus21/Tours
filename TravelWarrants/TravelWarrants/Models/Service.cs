namespace TravelWarrants.Models
{
    public class Service
    {
        public Service()
        {
            this.InoviceService = new HashSet<InoviceService>();
            this.Inovice = new HashSet<Inovice>();
            this.Inovice1 = new HashSet<Inovice>();
            this.Inovice2 = new HashSet<Inovice>();
            this.Inovice3 = new HashSet<Inovice>();
            this.Inovice4 = new HashSet<Inovice>();

        }

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
