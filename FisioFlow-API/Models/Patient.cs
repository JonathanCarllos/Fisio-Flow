namespace FisioFlow_API.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
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
        public string? Insurance { get; set; }
        public string? MedicalHistory { get; set; }  
        public bool Status { get; set; }
        public string? Notes { get; set; }

        public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();
    }
}
