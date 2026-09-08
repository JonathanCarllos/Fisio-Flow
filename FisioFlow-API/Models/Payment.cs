using FisioFlow_API.Enums;

namespace FisioFlow_API.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime DueDate { get; set; }
        public Status Status { get; set; }
        public string? Description { get; set; }
        public string? InsuranceName { get; set; }
        public Treatment? Treatment { get; set; }
        public int TreatmentId { get; set; }
        public Patient? Patient { get; set; }
        public int PatientId { get; set; }

    }
}
