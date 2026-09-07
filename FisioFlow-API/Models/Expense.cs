using FisioFlow_API.Enums;

namespace FisioFlow_API.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public bool Status { get; set; }
        public Category Category { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

    }
}
