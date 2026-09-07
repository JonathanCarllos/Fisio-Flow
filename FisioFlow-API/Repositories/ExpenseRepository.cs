using FisioFlow_API.Context;
using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FisioFlow_API.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;

        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await _context.Expenses
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Expense> GetExpenseByIdAsync(int expenseId)
        {
            return _context.Expenses
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ExpenseId == expenseId);
        }

        public async Task<Expense> CreateExpenseAsync(Expense expense)
        {
            if(expense is null)
                throw new ArgumentNullException(nameof(expense));

            await _context.Expenses.AddAsync(expense);

            return expense;
        }

        public async Task<Expense> UpdateExpenseAsync(Expense expense)
        {
            if(expense is null)
                throw new ArgumentNullException(nameof(expense));

            _context.Expenses.Update(expense);

            return expense;
        }
        public async Task<Expense> DeleteExpenseAsync(int expenseId)
        {
           var expense = await _context.Expenses.FindAsync(expenseId);

            if (expense is null)
                throw new KeyNotFoundException($"Expense with ID {expenseId} not found.");

            _context.Expenses.Remove(expense);

            return expense;
        }
    }
}
