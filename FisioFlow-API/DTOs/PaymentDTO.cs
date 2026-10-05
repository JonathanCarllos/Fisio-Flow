using FisioFlow_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace FisioFlow_API.DTOs
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }

        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public DateTime? PaymentDate { get; set; }

        public DateTime? DueDate { get; set; }

        public Status Status { get; set; }

        public string? Description { get; set; }

        public string? InsuranceName { get; set; }


        public int TreatmentId { get; set; }

        public int PatientId { get; set; }



        // EXIBIÇÃO

        public string? PatientName { get; set; }

        public string? TreatmentName { get; set; }
    }
}