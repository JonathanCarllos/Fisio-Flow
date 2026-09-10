using System.Text.Json.Serialization;

namespace FisioFlow_API.Models
{
    public class Session
    {
        public int SessionId { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly Time { get; set; }

        public int Duration { get; set; }

        public bool Status { get; set; } = true;

        public string Notes { get; set; } = string.Empty;

        public string Evolution { get; set; } = string.Empty;



        public int PatientId { get; set; }

        [JsonIgnore]
        public Patient Patient { get; set; } = null!;



        public int PhysiotherapistId { get; set; }

        [JsonIgnore]
        public Physiotherapist Physiotherapist { get; set; } = null!;



        // Relacionamento Session -> MedicalRecord

        [JsonIgnore]
        public ICollection<MedicalRecord> MedicalRecords { get; set; }
            = new List<MedicalRecord>();

    }
}