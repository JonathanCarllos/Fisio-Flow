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
    }
}
