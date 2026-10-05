using FisioFlow_API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FisioFlow_API.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }


        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }


        [Required]
        public PaymentMethod PaymentMethod { get; set; }


        [Required]
        public DateTime? PaymentDate { get; set; }


        [Required]
        public DateTime? DueDate { get; set; }


        [Required]
        public Status Status { get; set; }


        [MaxLength(500)]
        public string? Description { get; set; }


        [MaxLength(150)]
        public string? InsuranceName { get; set; }


        [Required]
        public int TreatmentId { get; set; }

        [ForeignKey(nameof(TreatmentId))]
        public Treatment Treatment { get; set; } = null!;


        [Required]
        public int PatientId { get; set; }

        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; } = null!;
    }
}