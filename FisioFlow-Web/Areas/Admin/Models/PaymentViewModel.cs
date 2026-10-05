using FisioFlow_Web.Enums;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace FisioFlow_Web.Areas.Admin.Models
{
    public class PaymentViewModel
    {
        public int PaymentId { get; set; }


        [Required(ErrorMessage = "O valor do pagamento é obrigatório.")]
        [Range(0.01, 999999999.99, ErrorMessage = "O valor deve ser maior que zero.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Valor")]
        public decimal Amount { get; set; }


        [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
        [Display(Name = "Método de pagamento")]
        public PaymentMethod PaymentMethod { get; set; }


        [Required(ErrorMessage = "A data do pagamento é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data do pagamento")]
        public DateTime? PaymentDate { get; set; }


        [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de vencimento")]
        public DateTime? DueDate { get; set; }


        [Required(ErrorMessage = "O status do pagamento é obrigatório.")]
        [Display(Name = "Status")]
        public Status Status { get; set; }


        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        [Display(Name = "Descrição")]
        public string? Description { get; set; }


        [StringLength(150, ErrorMessage = "O nome do convênio deve ter no máximo 150 caracteres.")]
        [Display(Name = "Convênio")]
        public string? InsuranceName { get; set; }


        [Required(ErrorMessage = "O tratamento é obrigatório.")]
        [Display(Name = "Tratamento")]
        public int TreatmentId { get; set; }


        [Required(ErrorMessage = "O paciente é obrigatório.")]
        [Display(Name = "Paciente")]
        public int PatientId { get; set; }


        // Campos auxiliares para exibição
        public string? PatientName { get; set; }

        public string? TreatmentName { get; set; }
    }
}