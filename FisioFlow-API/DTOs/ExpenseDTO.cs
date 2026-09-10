using FisioFlow_API.Enums;

namespace FisioFlow_API.DTOs
{
    public class ExpenseDTO
    {
        public int ExpenseId { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string? Notes { get; set; }

        public bool Status { get; set; } = true;

        public Category Category { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
    }
}