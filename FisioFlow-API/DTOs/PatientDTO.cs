namespace FisioFlow_API.DTOs
{
    public class PatientDTO
    {
        public int PatientId { get; set; }


        public string Name { get; set; } = string.Empty;


        public string CPF { get; set; } = string.Empty;


        public string Email { get; set; } = string.Empty;


        public string RG { get; set; } = string.Empty;


        public DateTime BirthDate { get; set; }


        public string Phone { get; set; } = string.Empty;


        public string Address { get; set; } = string.Empty;


        public string Neighborhood { get; set; } = string.Empty;


        public string City { get; set; } = string.Empty;


        public string State { get; set; } = string.Empty;


        public string PostalCode { get; set; } = string.Empty;


        public string? Insurance { get; set; }


        public string? MedicalHistory { get; set; }


        public bool Status { get; set; } = true;


        public string? Notes { get; set; }
    }
}