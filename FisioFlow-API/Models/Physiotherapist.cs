using System.Text.Json.Serialization;

namespace FisioFlow_API.Models
{
    public class Physiotherapist
    {
        public int PhysiotherapistId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Crefito { get; set; } = string.Empty;

        public string Specialty { get; set; } = string.Empty;

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

        public string AvailableHours { get; set; } = string.Empty;

        public bool Status { get; set; } = true;

        public string Color { get; set; } = string.Empty;


        [JsonIgnore]
        public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>();

        [JsonIgnore]
        public ICollection<Session> Sessions { get; set; } = new List<Session>();

        [JsonIgnore]
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    }
}