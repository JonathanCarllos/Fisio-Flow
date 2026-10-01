using FisioFlow_Web.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FisioFlow_Web.Areas.Admin.Models
{
    public class ExpenseViewModel
    {
        [Key]
        public int ExpenseId { get; set; }


        [Required]
        [StringLength(150)]
        public string Description { get; set; } = string.Empty;


        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }


        public DateTime Date { get; set; }


        [StringLength(1000)]
        public string? Notes { get; set; }


        public bool Status { get; set; } = true;


        public Category Category { get; set; }


        public PaymentMethod PaymentMethod { get; set; }
    }
}
