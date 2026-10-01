using FisioFlow_Web.Areas.Admin.Models;

namespace FisioFlow_Web.Areas.Admin.Services.Interfaces
{
    public interface IExpenseServices
    {
        Task<IEnumerable<ExpenseViewModel>> GetAllExpensesAsync();
        Task<ExpenseViewModel> GetExpenseByIdAsync(int id);
        Task<ExpenseViewModel> CreateExpenseAsync(ExpenseViewModel expense);
        Task<ExpenseViewModel> UpdateExpenseAsync(ExpenseViewModel expense);
        Task<bool> DeleteExpenseAsync(int id);

    }
}
