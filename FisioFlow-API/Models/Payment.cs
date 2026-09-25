using FisioFlow_API.Enums;
using System.Text.Json.Serialization;

namespace FisioFlow_API.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }


        public PaymentMethod PaymentMethod { get; set; }


        public DateTime PaymentDate { get; set; }


        public DateTime DueDate { get; set; }


        public bool Status { get; set; }


        public string? Description { get; set; }


        public string? InsuranceName { get; set; }



        // Relacionamento Treatment

        public int TreatmentId { get; set; }

        [JsonIgnore]
        public Treatment? Treatment { get; set; }



        // Relacionamento Patient

        public int PatientId { get; set; }

        [JsonIgnore]
        public Patient? Patient { get; set; }

    }
}