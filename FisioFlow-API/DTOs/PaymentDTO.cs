using FisioFlow_API.Enums;
using System.ComponentModel.DataAnnotations;

namespace FisioFlow_API.DTOs
{
    public class PaymentDTO
    {

        public int PaymentId { get; set; }


        [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
        public PaymentMethod PaymentMethod { get; set; }



        [Required(ErrorMessage = "A data do pagamento é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime? PaymentDate { get; set; }



        [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }



        [Required(ErrorMessage = "O status do pagamento é obrigatório.")]
        public Status Status { get; set; }



        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string? Description { get; set; }



        [StringLength(150, ErrorMessage = "O nome do convênio deve ter no máximo 150 caracteres.")]
        public string? InsuranceName { get; set; }



        [Required(ErrorMessage = "O tratamento é obrigatório.")]
        public int TreatmentId { get; set; }



        [Required(ErrorMessage = "O paciente é obrigatório.")]
        public int PatientId { get; set; }

    }
}