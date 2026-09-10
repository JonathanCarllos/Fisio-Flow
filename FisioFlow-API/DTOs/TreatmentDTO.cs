using System.ComponentModel.DataAnnotations;

namespace FisioFlow_API.DTOs
{
    public class TreatmentDTO
    {

        public int TreatmentId { get; set; }


        [Required(ErrorMessage = "O tipo de tratamento é obrigatório.")]
        [StringLength(100, ErrorMessage = "O tipo deve ter no máximo 100 caracteres.")]
        public string Type { get; set; } = string.Empty;



        [Required(ErrorMessage = "O diagnóstico é obrigatório.")]
        public string Diagnosis { get; set; } = string.Empty;



        [Required(ErrorMessage = "A quantidade total de sessões é obrigatória.")]
        [Range(1, 1000, ErrorMessage = "O total de sessões deve ser maior que zero.")]
        public int TotalSessions { get; set; }



        [Range(0, 1000, ErrorMessage = "A quantidade de sessões concluídas não pode ser negativa.")]
        public int CompletedSessions { get; set; }



        [StringLength(4000)]
        public string Exercises { get; set; } = string.Empty;



        [StringLength(4000)]
        public string Observations { get; set; } = string.Empty;



        public bool Status { get; set; } = true;



        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateTime StartDate { get; set; }



        [Required(ErrorMessage = "A data de término é obrigatória.")]
        public DateTime EndDate { get; set; }




        // Relacionamento com Patient

        [Required(ErrorMessage = "O paciente é obrigatório.")]
        public int PatientId { get; set; }



        // Relacionamento com Physiotherapist

        [Required(ErrorMessage = "O fisioterapeuta é obrigatório.")]
        public int PhysiotherapistId { get; set; }

    }
}