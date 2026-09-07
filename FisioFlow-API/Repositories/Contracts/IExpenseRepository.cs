using FisioFlow_API.Models;

namespace FisioFlow_API.Repositories.Contracts
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<Expense> GetExpenseByIdAsync(int expenseId);
        Task<Expense> CreateExpenseAsync(Expense expense);
        Task<Expense> UpdateExpenseAsync(Expense expense);
        Task<Expense> DeleteExpenseAsync(int expenseId);
    }
}
