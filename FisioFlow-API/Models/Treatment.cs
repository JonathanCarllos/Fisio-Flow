using System.Text.Json.Serialization;

namespace FisioFlow_API.Models
{
    public class Treatment
    {
        public int TreatmentId { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Diagnosis { get; set; } = string.Empty;

        public int TotalSessions { get; set; }

        public int CompletedSessions { get; set; }

        public string Exercises { get; set; } = string.Empty;

        public string Observations { get; set; } = string.Empty;

        public bool Status { get; set; } = true;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }



        public int PatientId { get; set; }

        [JsonIgnore]
        public Patient Patient { get; set; } = null!;



        public int PhysiotherapistId { get; set; }

        [JsonIgnore]
        public Physiotherapist Physiotherapist { get; set; } = null!;



        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}