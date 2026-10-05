using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FisioFlow_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TreatmentsController : Controller
    {
        private readonly ITreatmentServices _treatmentService;
        private readonly IPatientServices _patientService;
        private readonly IPhysiotherapistServices _physiotherapistService;


        public TreatmentsController(
            ITreatmentServices treatmentService,
            IPatientServices patientService,
            IPhysiotherapistServices physiotherapistService)
        {
            _treatmentService = treatmentService;
            _patientService = patientService;
            _physiotherapistService = physiotherapistService;
        }



        public async Task<IActionResult> Index()
        {
            var treatments = await _treatmentService.GetAllTreatmentsAsync();

            ViewBag.Total = treatments?.Count() ?? 0;

            return View(treatments);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadSelectLists();

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TreatmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadSelectLists();

                return View(model);
            }


            var result = await _treatmentService.CreateTreatmentAsync(model);


            if (result == null)
            {
                ModelState.AddModelError(
                    "",
                    "Não foi possível cadastrar o tratamento."
                );

                await LoadSelectLists();

                return View(model);
            }


            TempData["Success"] = "Tratamento cadastrado com sucesso!";


            return RedirectToAction(nameof(Index));
        }




        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var treatment = await _treatmentService.GetTreatmentByIdAsync(id);


            if (treatment == null)
                return NotFound();


            await LoadPeopleNames(treatment);


            return View(treatment);
        }





        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var treatment = await _treatmentService.GetTreatmentByIdAsync(id);


            if (treatment == null)
                return NotFound();


            await LoadSelectLists(
               treatment.PatientId,
               treatment.PhysiotherapistId
            );


            return View(treatment);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            TreatmentViewModel model)
        {

            if (id != model.TreatmentId)
                return BadRequest();



            if (!ModelState.IsValid)
            {
                await LoadSelectLists(
                    model.PatientId,
                    model.PhysiotherapistId
                );

                return View(model);
            }



            var result = await _treatmentService.UpdateTreatmentAsync(model);



            if (result == null)
            {
                ModelState.AddModelError(
                    "",
                    "Não foi possível atualizar o tratamento."
                );


                await LoadSelectLists(
                  model.PatientId,
                  model.PhysiotherapistId
                );

                return View(model);
            }



            TempData["Success"] = "Tratamento atualizado com sucesso!";


            return RedirectToAction(nameof(Index));
        }





        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var treatment = await _treatmentService.GetTreatmentByIdAsync(id);



            if (treatment == null)
                return NotFound();



            await LoadPeopleNames(treatment);



            return View(treatment);
        }






        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var success = await _treatmentService.DeleteTreatmentAsync(id);



            if (!success)
            {
                TempData["Error"] =
                    "Não foi possível excluir o tratamento.";

                return RedirectToAction(nameof(Delete), new { id });
            }



            TempData["Success"] =
                "Tratamento excluído com sucesso!";


            return RedirectToAction(nameof(Index));
        }






        private async Task LoadSelectLists(
      int? selectedPatientId = null,
      int? selectedPhysiotherapistId = null)
        {
            var patients = await _patientService.GetAllPatientsAsync();

            var physiotherapists = await _physiotherapistService.GetAll();


            ViewBag.Patients = new SelectList(
                patients,
                "PatientId",
                "Name",
                selectedPatientId
            );


            ViewBag.Physiotherapists = new SelectList(
                physiotherapists,
                "PhysiotherapistId",
                "Name",
                selectedPhysiotherapistId
            );
        }






        private async Task LoadPeopleNames(
            TreatmentViewModel treatment)
        {

            var patient =
                await _patientService.GetPatientByIdAsync(
                    treatment.PatientId
                );


            var physiotherapist =
                await _physiotherapistService
                .GetPhysiotherapistByIDAsync(
                    treatment.PhysiotherapistId
                );



            ViewBag.PatientName =
                patient?.Name ?? "Não informado";



            ViewBag.PhysiotherapistName =
                physiotherapist?.Name ?? "Não informado";
        }
    }
}