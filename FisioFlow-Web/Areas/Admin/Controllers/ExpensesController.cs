using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ExpensesController : Controller
    {
        private readonly IExpenseServices _expenseService;

        public ExpensesController(IExpenseServices expenseService)
        {
            _expenseService = expenseService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _expenseService.GetAllExpensesAsync();

            if (result == null)
                return NotFound();

            var expenses = result.ToList();

            var total = expenses.Count();

            var totalAmount = expenses.Sum(x => x.Amount);

            ViewBag.Total = total;
            ViewBag.TotalAmount = totalAmount;

            return View(expenses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseViewModel expenseVM)
        {
            if (!ModelState.IsValid)
                return View(expenseVM);


            var result = await _expenseService.CreateExpenseAsync(expenseVM);


            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }


            ModelState.AddModelError("", "Não foi possível cadastrar a despesa.");

            return View(expenseVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _expenseService.GetExpenseByIdAsync(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ExpenseViewModel expenseVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _expenseService.UpdateExpenseAsync(expenseVM);

                if (result is not null)
                    return RedirectToAction(nameof(Index));
            }

            return View(expenseVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            var result = await _expenseService.GetExpenseByIdAsync(id);



            if (result == null)
            {
                return NotFound();
            }



            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult<ExpenseViewModel>> Delete(int id)
        {
            var result = await _expenseService.GetExpenseByIdAsync(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost(), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {

            var result = await _expenseService.DeleteExpenseAsync(Id);


            if (result)
            {
                TempData["Success"] = "Despesa excluída com sucesso.";
            }
            else
            {
                TempData["Error"] = "Erro ao excluir despesa.";
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
