using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;

namespace FisioFlow_API.Service
{
    public class ExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Expense> CreateExpenseAsync(Expense expense)
        {
            await _unitOfWork.ExpenseRepository.CreateExpenseAsync(expense);

            await _unitOfWork.Commit();

            return expense;
        }
    }
}
