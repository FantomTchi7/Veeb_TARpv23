namespace Veeb_TARpv23.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ConsumptionQuantity { get; set; }
        public int Amount { get; set; }
        public int PaymentStatusId { get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; } 
        public int ConsumerId { get; set; }
        public virtual Consumer Consumer { get; set; } 
    }
}
