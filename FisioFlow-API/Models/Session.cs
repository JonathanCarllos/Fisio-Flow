namespace FisioFlow_API.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int Duration { get; set; } // Duration in minutes
        public bool Status { get; set; } 
        public string Notes { get; set; }
        public string Evolution { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
        public Physiotherapist Physiotherapist { get; set; }
        public int PhysiotherapistId { get; set; }

    }
}
