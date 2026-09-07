using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpensesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetAllExpenses()
        {
            var expenses = await _unitOfWork.ExpenseRepository
                .GetAllExpensesAsync();

            return Ok(expenses);
        }

        //Get: api/Expenses/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Expense>> GetExpenseById(int id)
        {
            var expenses = await _unitOfWork.ExpenseRepository.GetExpenseByIdAsync(id);

            if (expenses is null)
            {
                return NotFound(new
                {
                    message = $"Expense com ID {id} não encontrado."
                });
            }

            return Ok(expenses);
        }

        // POST: api/Expenses
        [HttpPost]
        public async Task<ActionResult<Expense>> UpdateExpense(Expense expense)
        {
            if(expense is null)
            {
                return BadRequest(new
                {
                    message = "Os dados do expense são obrigatórios."
                });
            }

            await _unitOfWork.ExpenseRepository.CreateExpenseAsync(expense);

            await _unitOfWork.Commit();

            return CreatedAtAction(nameof(GetExpenseById), new {id = expense.ExpenseId}, expense);
        }

        //Put: api/Expenses/id
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Patient>> UpdatePatient(
            int id,
            Expense expense)
        {
            if (expense is null)
            {
                return BadRequest(new
                {
                    message = "Os dados do expense são obrigatórios."
                });
            }

            if (id != expense.ExpenseId)
            {
                return BadRequest(new
                {
                    message = "O ID da URL é diferente do ID do expense."
                });
            }

            var newExpense = await _unitOfWork.ExpenseRepository.GetExpenseByIdAsync(id);

            if(newExpense is null)
            {
                return NotFound(new
                {
                    messeger = $"expense com o id= {id} não encontrado"
                });
            }

            await _unitOfWork.ExpenseRepository.UpdateExpenseAsync(newExpense);

            await _unitOfWork.Commit();

            return Ok(newExpense);
        }

        //Delete api/Expenses/id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Expense>> DeleteExpense(int id)
        {
            try
            {
                var expense = _unitOfWork.ExpenseRepository.DeleteExpenseAsync(id);

                await _unitOfWork.Commit();

                return Ok(expense);
            }
            catch(KeyNotFoundException)
            {
                return NotFound(new
                {
                    message = $"Expense com ID {id} não encontrado."
                });
            }
        }


    }
}
