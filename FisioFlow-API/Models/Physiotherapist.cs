namespace FisioFlow_API.Models
{
    public class Physiotherapist
    {
        public int PhysiotherapistId { get; set; }
        public string Name { get; set; }
        public string Crefito { get; set; }
        public string Specialty { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string RG { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string AvailableHours { get; set; }
        public bool Status { get; set; }
        public string Color { get; set; }

        public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    }
}
