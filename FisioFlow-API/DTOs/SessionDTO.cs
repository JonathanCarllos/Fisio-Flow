using System.ComponentModel.DataAnnotations;

namespace FisioFlow_API.DTOs
{
    public class SessionDTO
    {

        public int SessionId { get; set; }


        [Required(ErrorMessage = "A data da sessão é obrigatória.")]
        public DateOnly Date { get; set; }



        [Required(ErrorMessage = "O horário da sessão é obrigatório.")]
        public TimeOnly Time { get; set; }



        [Required(ErrorMessage = "A duração é obrigatória.")]
        [Range(1, 480, ErrorMessage = "A duração deve estar entre 1 e 480 minutos.")]
        public int Duration { get; set; }



        public bool Status { get; set; } = true;



        [StringLength(1000, ErrorMessage = "As observações devem ter no máximo 1000 caracteres.")]
        public string Notes { get; set; } = string.Empty;



        [StringLength(4000, ErrorMessage = "A evolução deve ter no máximo 4000 caracteres.")]
        public string Evolution { get; set; } = string.Empty;




        // Relacionamento com Patient

        [Required(ErrorMessage = "O paciente é obrigatório.")]
        public int PatientId { get; set; }




        // Relacionamento com Physiotherapist

        [Required(ErrorMessage = "O fisioterapeuta é obrigatório.")]
        public int PhysiotherapistId { get; set; }

    }
}