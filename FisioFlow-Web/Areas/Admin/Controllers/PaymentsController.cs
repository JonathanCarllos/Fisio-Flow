using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FisioFlow_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PaymentsController : Controller
    {

        private readonly IPaymentServices _paymentService;
        private readonly IPatientServices _patientService;
        private readonly ITreatmentServices _treatmentService;



        public PaymentsController(
            IPaymentServices paymentService,
            IPatientServices patientService,
            ITreatmentServices treatmentService)
        {
            _paymentService = paymentService;
            _patientService = patientService;
            _treatmentService = treatmentService;
        }




        private async Task LoadSelects()
        {
            var patients = await _patientService.GetAllPatientsAsync();

            var treatments = await _treatmentService.GetAllTreatmentsAsync();



            if (patients == null)
            {
                patients = new List<PatientViewModel>();
            }


            if (treatments == null)
            {
                treatments = new List<TreatmentViewModel>();
            }




            ViewBag.Patients = new SelectList(
                patients,
                "PatientId",
                "Name"
            );




            ViewBag.Treatments = new SelectList(
                treatments,
                "TreatmentId",
                "Type"
            );
        }







        public async Task<IActionResult> Index()
        {

            var payments =
                await _paymentService.GetAllPaymentsAsync();



            if (payments == null)
            {
                payments = new List<PaymentViewModel>();
            }



            return View(payments);

        }








        [HttpGet]
        public async Task<IActionResult> Create()
        {

            await LoadSelects();


            return View(new PaymentViewModel());

        }








        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            PaymentViewModel paymentVM)
        {


            if (!ModelState.IsValid)
            {

                await LoadSelects();

                return View(paymentVM);

            }




            var result =
                await _paymentService
                .CreatePaymentAsync(paymentVM);




            if (result == null)
            {

                ModelState.AddModelError(
                    "",
                    "Erro ao cadastrar pagamento."
                );


                await LoadSelects();


                return View(paymentVM);

            }





            TempData["Success"] =
                "Pagamento cadastrado com sucesso!";



            return RedirectToAction(nameof(Index));

        }







        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var payment =
                await _paymentService.GetPaymentByIdAsync(id);



            if (payment == null)
                return NotFound();




            await LoadSelects();



            return View(payment);

        }








        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            PaymentViewModel paymentVM)
        {


            if (id != paymentVM.PaymentId)
                return NotFound();




            if (!ModelState.IsValid)
            {

                await LoadSelects();

                return View(paymentVM);

            }





            var result =
                await _paymentService
                .UpdatePaymentAsync(paymentVM);




            if (result == null)
            {

                ModelState.AddModelError(
                    "",
                    "Erro ao atualizar pagamento."
                );


                await LoadSelects();


                return View(paymentVM);

            }




            TempData["Success"] =
                "Pagamento atualizado com sucesso!";



            return RedirectToAction(nameof(Index));

        }








        public async Task<IActionResult> Details(int id)
        {

            var payment =
                await _paymentService.GetPaymentByIdAsync(id);



            if (payment == null)
                return NotFound();



            return View(payment);

        }








        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            var payment =
                await _paymentService.GetPaymentByIdAsync(id);



            if (payment == null)
                return NotFound();



            return View(payment);

        }








        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var result =
                await _paymentService.DeletePaymentAsync(id);



            if (!result)
            {

                TempData["Error"] =
                    "Erro ao excluir pagamento.";

                return RedirectToAction(nameof(Index));

            }




            TempData["Success"] =
                "Pagamento excluído com sucesso!";



            return RedirectToAction(nameof(Index));

        }

    }
}