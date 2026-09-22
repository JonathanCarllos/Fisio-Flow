using System.ComponentModel.DataAnnotations;

namespace FisioFlow_Web.Areas.Admin.Models
{
    public class PatientViewModel
    {
        public int PatientId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 150 caracteres.")]
        [Display(Name = "Nome")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [RegularExpression(
     @"^\d{3}\.\d{3}\.\d{3}-\d{2}$|^\d{11}$",
     ErrorMessage = "CPF inválido."
 )]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        [StringLength(150)]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O RG é obrigatório.")]
        [StringLength(20, ErrorMessage = "O RG deve ter no máximo 20 caracteres.")]
        [Display(Name = "RG")]
        public string RG { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone(ErrorMessage = "Telefone inválido.")]
        [StringLength(20, ErrorMessage = "O telefone deve ter no máximo 20 caracteres.")]
        [Display(Name = "Telefone")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [StringLength(200, ErrorMessage = "O endereço deve ter no máximo 200 caracteres.")]
        [Display(Name = "Endereço")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "O bairro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O bairro deve ter no máximo 100 caracteres.")]
        [Display(Name = "Bairro")]
        public string Neighborhood { get; set; } = string.Empty;

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(100, ErrorMessage = "A cidade deve ter no máximo 100 caracteres.")]
        [Display(Name = "Cidade")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Informe a UF com 2 caracteres.")]
        [Display(Name = "UF")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "O CEP deve conter 9 caracteres.")]
        [Display(Name = "CEP")]
        public string PostalCode { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "O convênio deve ter no máximo 100 caracteres.")]
        [Display(Name = "Convênio")]
        public string? Insurance { get; set; }

        [StringLength(1000, ErrorMessage = "O histórico médico deve ter no máximo 1000 caracteres.")]
        [Display(Name = "Histórico Médico")]
        [DataType(DataType.MultilineText)]
        public string? MedicalHistory { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; } = true;

        [StringLength(1000, ErrorMessage = "As observações devem ter no máximo 1000 caracteres.")]
        [Display(Name = "Observações")]
        [DataType(DataType.MultilineText)]
        public string? Notes { get; set; }
    }
}