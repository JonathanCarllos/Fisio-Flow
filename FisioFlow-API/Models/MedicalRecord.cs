using FisioFlow_API.Enums;

namespace FisioFlow_API.Models
{
    public class MedicalRecord
    {
        public int MedicalRecordId { get; set; }

        public RecordType RecordType { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string? FunctionalDiagnosis { get; set; }

        public string? FileUrl { get; set; }

        // Relacionamento com Patient
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        // Relacionamento com Physiotherapist
        public int PhysiotherapistId { get; set; }
        public Physiotherapist? Physiotherapist { get; set; }

        // Relacionamento opcional com Session
        public int? SessionId { get; set; }
        public Session? Session { get; set; }
    }
}