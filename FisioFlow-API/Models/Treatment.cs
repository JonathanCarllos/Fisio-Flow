namespace FisioFlow_API.Models
{
    public class Treatment
    {
        public int TreatmentId { get; set; }
        public string Type { get; set; }
        public string Diagnosis { get; set; }
        public int TotalSessions { get; set; }
        public int CompletedSessions { get; set; }
        public string Exercises { get; set; }
        public string Observations { get; set; }
        public bool Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public Physiotherapist Physiotherapist { get; set; }
        public int PhysiotherapistId { get; set; }

    }
}
