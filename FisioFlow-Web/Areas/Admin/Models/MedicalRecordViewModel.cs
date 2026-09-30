using FisioFlow_Web.Enums;

namespace FisioFlow_Web.Areas.Admin.Models
{
    public class MedicalRecordViewModel
    {
        public int MedicalRecordId { get; set; }

        public RecordType RecordType { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string? FunctionalDiagnosis { get; set; }

        public string? FileUrl { get; set; }

        // FK Patient
        public int PatientId { get; set; }

        // Nome do paciente - retorno da API
        public string? PatientName { get; set; }

        // FK Physiotherapist
        public int PhysiotherapistId { get; set; }

        // Nome do fisioterapeuta - retorno da API
        public string? PhysiotherapistName { get; set; }

        // FK Session opcional
        public int? SessionId { get; set; }
    }
}