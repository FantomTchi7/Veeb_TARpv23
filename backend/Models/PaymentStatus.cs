namespace Veeb_TARpv23.Models
{
    public class PaymentStatus
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime DueDate { get; set; }
        public int AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
