namespace Veeb_TARpv23.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int InvoiceLineId { get; set; }
        public virtual InvoiceLine InvoiceLine { get; set; }
        public int TotalAmount { get; set; }
        public int PaymentStatusId { get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}