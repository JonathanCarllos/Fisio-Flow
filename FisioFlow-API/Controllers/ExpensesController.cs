using AutoMapper;
using FisioFlow_API.DTOs;
using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExpensesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseDTO>>> GetAllExpenses()
        {
            var expenses = await _unitOfWork.ExpenseRepository.GetAllExpensesAsync();

            var expenseDTOs = _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);

            return Ok(expenseDTOs);
        }

        // GET: api/Expenses/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ExpenseDTO>> GetExpenseById(int id)
        {
            var expense = await _unitOfWork.ExpenseRepository.GetExpenseByIdAsync(id);

            if (expense is null)
            {
                return NotFound(new
                {
                    message = $"Expense com ID {id} não encontrado."
                });
            }

            return Ok(_mapper.Map<ExpenseDTO>(expense));
        }

        // POST: api/Expenses
        [HttpPost]
        public async Task<ActionResult<ExpenseDTO>> CreateExpense(ExpenseDTO expenseDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var expense = _mapper.Map<Expense>(expenseDTO);

            await _unitOfWork.ExpenseRepository.CreateExpenseAsync(expense);

            await _unitOfWork.Commit();

            var expenseCreated = _mapper.Map<ExpenseDTO>(expense);

            return CreatedAtAction(
                nameof(GetExpenseById),
                new { id = expenseCreated.ExpenseId },
                expenseCreated);
        }

        // PUT: api/Expenses/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ExpenseDTO>> UpdateExpense(int id, ExpenseDTO expenseDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != expenseDTO.ExpenseId)
            {
                return BadRequest(new
                {
                    message = "O ID da URL é diferente do ID da despesa."
                });
            }

            var expense = await _unitOfWork.ExpenseRepository.GetExpenseByIdAsync(id);

            if (expense is null)
            {
                return NotFound(new
                {
                    message = $"Expense com ID {id} não encontrado."
                });
            }

            _mapper.Map(expenseDTO, expense);

            await _unitOfWork.ExpenseRepository.UpdateExpenseAsync(expense);

            await _unitOfWork.Commit();

            return Ok(_mapper.Map<ExpenseDTO>(expense));
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ExpenseDTO>> DeleteExpense(int id)
        {
            try
            {
                var expense = await _unitOfWork.ExpenseRepository.DeleteExpenseAsync(id);

                await _unitOfWork.Commit();

                return Ok(_mapper.Map<ExpenseDTO>(expense));
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    message = $"Expense com ID {id} não encontrado."
                });
            }
        }
    }
}