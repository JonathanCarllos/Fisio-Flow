using System.ComponentModel.DataAnnotations;

namespace FisioFlow_Web.Areas.Admin.Models
{
    public class SessionViewModel
    {

        public int SessionId { get; set; }



        [Required(ErrorMessage = "A data da sessão é obrigatória.")]
        public DateOnly Date { get; set; }



        [Required(ErrorMessage = "O horário da sessão é obrigatório.")]
        public TimeOnly Time { get; set; }



        [Required(ErrorMessage = "A duração é obrigatória.")]
        [Range(1, 480,
            ErrorMessage = "A duração deve estar entre 1 e 480 minutos.")]
        public int Duration { get; set; }



        public bool Status { get; set; } = true;



        [StringLength(1000)]
        public string? Notes { get; set; }



        [StringLength(4000)]
        public string? Evolution { get; set; }




        // ==========================
        // PACIENTE
        // ==========================

        [Required(ErrorMessage = "Selecione um paciente.")]
        public int PatientId { get; set; }


        public string? PatientName { get; set; }




        // ==========================
        // FISIOTERAPEUTA
        // ==========================

        [Required(ErrorMessage = "Selecione um fisioterapeuta.")]
        public int PhysiotherapistId { get; set; }


        public string? PhysiotherapistName { get; set; }

        public string? PhysiotherapistColor { get; set; }





        // ==========================
        // LISTAS PARA DROPDOWN
        // ==========================


        public IEnumerable<PatientViewModel> Patients { get; set; }
            = new List<PatientViewModel>();



        public IEnumerable<PhysiotherapistViewModel> Physiotherapists { get; set; }
            = new List<PhysiotherapistViewModel>();


    }
}