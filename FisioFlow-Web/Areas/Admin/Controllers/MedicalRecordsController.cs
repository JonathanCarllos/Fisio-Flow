using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MedicalRecordsController : Controller
    {
        private readonly IMedicalRecordServices _medicalRecordServices;
        private readonly IPatientServices _patientServices;
        private readonly IPhysiotherapistServices _physiotherapistServices;

        public MedicalRecordsController(
            IMedicalRecordServices medicalRecordServices,
            IPatientServices patientServices,
            IPhysiotherapistServices physiotherapistServices)
        {
            _medicalRecordServices = medicalRecordServices;
            _patientServices = patientServices;
            _physiotherapistServices = physiotherapistServices;
        }

        // ============================================================
        // GET: /Admin/MedicalRecords
        // ============================================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _medicalRecordServices.GetAll();

            return View(result);
        }

        // ============================================================
        // GET: /Admin/MedicalRecords/Create
        // ============================================================

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadFormDataAsync();

            return View(new MedicalRecordViewModel());
        }

        // ============================================================
        // POST: /Admin/MedicalRecords/Create
        // ============================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            MedicalRecordViewModel medicalVM)
        {
            if (!ModelState.IsValid)
            {
                await LoadFormDataAsync();
                return View(medicalVM);
            }

            var result = await _medicalRecordServices
                .CreateMedicalRecordAsync(medicalVM);

            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(
                string.Empty,
                "Não foi possível cadastrar o prontuário."
            );

            await LoadFormDataAsync();

            return View(medicalVM);
        }

        // ============================================================
        // GET: /Admin/MedicalRecords/Details/5
        // ============================================================

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _medicalRecordServices.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // ============================================================
        // GET: /Admin/MedicalRecords/Edit/5
        // ============================================================

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _medicalRecordServices.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            await LoadFormDataAsync();

            return View(result);
        }

        // ============================================================
        // POST: /Admin/MedicalRecords/Edit
        // ============================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            MedicalRecordViewModel medicalVM)
        {
            if (!ModelState.IsValid)
            {
                await LoadFormDataAsync();
                return View(medicalVM);
            }

            var result = await _medicalRecordServices
                .UpdateMedicalRecordAsync(medicalVM);

            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(
                string.Empty,
                "Não foi possível atualizar o prontuário."
            );

            await LoadFormDataAsync();

            return View(medicalVM);
        }

        // ============================================================
        // GET: /Admin/MedicalRecords/Delete/5
        // ============================================================

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _medicalRecordServices.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // ============================================================
        // POST: /Admin/MedicalRecords/Delete/5
        // ============================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _medicalRecordServices
                .DeleteMedicalRecordAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        // ============================================================
        // CARREGAR PACIENTES E FISIOTERAPEUTAS
        // ============================================================

        private async Task LoadFormDataAsync()
        {
            var patientsTask = _patientServices
                .GetAllPatientsAsync();

            var physiotherapistsTask = _physiotherapistServices
                .GetAll();

            await Task.WhenAll(
                patientsTask,
                physiotherapistsTask
            );

            ViewBag.Patients = await patientsTask;
            ViewBag.Physiotherapists = await physiotherapistsTask;
        }
    }
}