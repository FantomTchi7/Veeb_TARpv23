namespace Veeb_TARpv23.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ContactDetailsId { get; set; }
        public virtual ContactDetails ContactDetails { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}