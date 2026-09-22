using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PatientsController : Controller
    {
        private readonly IPatientServices _patientService;

        public PatientsController(IPatientServices patientService)
        {
            _patientService = patientService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _patientService.GetAllPatientsAsync();


            if (result == null)
                return NotFound();


            var total = result.Count();


            ViewBag.Total = total;


            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientViewModel patientVM)
        {
            if (!ModelState.IsValid)
                return View(patientVM);


            var result = await _patientService.CreatePatientAsync(patientVM);


            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }


            ModelState.AddModelError("", "Não foi possível cadastrar o paciente.");

            return View(patientVM);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _patientService.GetPatientByIdAsync(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PatientViewModel patientVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _patientService.UpdatePatientAsync(patientVM);

                if (result is not null)
                    return RedirectToAction(nameof(Index));
            }

            return View(patientVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            var patient = await _patientService.GetPatientByIdAsync(id);



            if (patient == null)
            {
                return NotFound();
            }



            return View(patient);

        }

        [HttpGet]
        public async Task<ActionResult<PatientViewModel>> Delete(int id)
        {
            var result = await _patientService.GetPatientByIdAsync(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost(), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int patientId)
        {

            var result = await _patientService.DeletePatientAsync(patientId);


            if (result)
            {
                TempData["Success"] = "Paciente excluído com sucesso.";
            }
            else
            {
                TempData["Error"] = "Erro ao excluir paciente.";
            }


            return RedirectToAction(nameof(Index));
        }

    }
}
