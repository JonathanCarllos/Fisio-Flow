namespace FisioFlow_API.DTOs
{
    public class TreatmentDTO
    {
        public int TreatmentId { get; set; }


        public string Type { get; set; } = string.Empty;


        public string Diagnosis { get; set; } = string.Empty;


        public int TotalSessions { get; set; }


        public int CompletedSessions { get; set; }


        public string Exercises { get; set; } = string.Empty;


        public string Observations { get; set; } = string.Empty;


        public bool Status { get; set; }


        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }



        public int PatientId { get; set; }


        public string? PatientName { get; set; }



        public int PhysiotherapistId { get; set; }


        public string? PhysiotherapistName { get; set; }
    }
}