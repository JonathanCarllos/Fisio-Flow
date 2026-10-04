using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PaymentsController : Controller
    {
        private readonly IPaymentServices _paymentService;


        public PaymentsController(IPaymentServices paymentService)
        {
            _paymentService = paymentService;
        }



        // LISTAGEM
        public async Task<IActionResult> Index()
        {
            var result = await _paymentService.GetAllPaymentsAsync();

            if (result == null)
                return NotFound();


            var payments = result.ToList();


            ViewBag.Total = payments.Count();


            return View(payments);
        }

        // DETALHES
        public async Task<IActionResult> Details(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);


            if (payment == null)
                return NotFound();


            return View(payment);
        }

        // CREATE GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentViewModel paymentVM)
        {

            if (!ModelState.IsValid)
                return View(paymentVM);



            var result = await _paymentService.CreatePaymentAsync(paymentVM);



            if (result == null)
            {
                ModelState.AddModelError(
                    "",
                    "Não foi possível cadastrar o pagamento."
                );

                return View(paymentVM);
            }



            TempData["Success"] = "Pagamento cadastrado com sucesso!";


            return RedirectToAction(nameof(Index));
        }

        // EDIT GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var payment = await _paymentService.GetPaymentByIdAsync(id);


            if (payment == null)
                return NotFound();



            return View(payment);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            PaymentViewModel paymentVM)
        {

            if (id != paymentVM.PaymentId)
                return NotFound();



            if (!ModelState.IsValid)
                return View(paymentVM);




            var result = await _paymentService.UpdatePaymentAsync(paymentVM);



            if (result == null)
            {
                ModelState.AddModelError(
                    "",
                    "Não foi possível atualizar o pagamento."
                );

                return View(paymentVM);
            }




            TempData["Success"] = "Pagamento atualizado com sucesso!";


            return RedirectToAction(nameof(Index));
        }

        // DELETE GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var payment = await _paymentService.GetPaymentByIdAsync(id);



            if (payment == null)
                return NotFound();



            return View(payment);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var result = await _paymentService.DeletePaymentAsync(id);



            if (!result)
            {
                TempData["Error"] =
                    "Não foi possível excluir o pagamento.";

                return RedirectToAction(nameof(Index));
            }




            TempData["Success"] =
                "Pagamento excluído com sucesso!";



            return RedirectToAction(nameof(Index));
        }

    }
}