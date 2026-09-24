using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static System.Collections.Specialized.BitVector32;

namespace FisioFlow_Web.Areas.Admin.Models
{
    public class PhysiotherapistViewModel
    {
        public int PhysiotherapistId { get; set; }


        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;


        [Required(ErrorMessage = "O CREFITO é obrigatório.")]
        [StringLength(30)]
        public string Crefito { get; set; } = string.Empty;


        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        [StringLength(100)]
        public string Specialty { get; set; } = string.Empty;


        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14, MinimumLength = 14)]
        public string CPF { get; set; } = string.Empty;


        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "O RG é obrigatório.")]
        [StringLength(20)]
        public string RG { get; set; } = string.Empty;


        [Required]
        public DateTime BirthDate { get; set; }


        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone]
        [StringLength(20)]
        public string Phone { get; set; } = string.Empty;


        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;


        [Required]
        [StringLength(100)]
        public string Neighborhood { get; set; } = string.Empty;


        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;


        [Required]
        [StringLength(2)]
        public string State { get; set; } = string.Empty;


        [Required]
        [StringLength(9)]
        public string PostalCode { get; set; } = string.Empty;


        [Required]
        [StringLength(200)]
        public string AvailableHours { get; set; } = string.Empty;


        public bool Status { get; set; } = true;


        [Required]
        [StringLength(50)]
        public string Color { get; set; } = string.Empty;

    }
}