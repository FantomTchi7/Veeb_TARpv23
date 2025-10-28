namespace Veeb_TARpv23.Models
{
    public class InvoiceLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } 
        public int Quantity { get; set; }
    }
}