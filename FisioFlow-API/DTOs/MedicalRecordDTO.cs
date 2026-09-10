using FisioFlow_API.Enums;

namespace FisioFlow_API.DTOs
{
    public class MedicalRecordDTO
    {
        public int MedicalRecordId { get; set; }


        public RecordType RecordType { get; set; }


        public string Title { get; set; } = string.Empty;


        public string Content { get; set; } = string.Empty;


        public string? FunctionalDiagnosis { get; set; }


        public string? FileUrl { get; set; }


        // FK Patient
        public int PatientId { get; set; }


        // FK Physiotherapist
        public int PhysiotherapistId { get; set; }


        // FK Session opcional
        public int? SessionId { get; set; }
    }
}