namespace Veeb_TARpv23.Models
{
    public class Consumer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ContactInfoId { get; set; }
        public virtual ContactInfo ContactInfo { get; set; } 
        public int LocationId { get; set; }
        public virtual Location Location { get; set; } 
    }
}
