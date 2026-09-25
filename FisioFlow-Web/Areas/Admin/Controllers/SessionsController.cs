using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SessionsController : Controller
    {

        private readonly ISessionServices _sessionService;
        private readonly IPatientServices _patientService;
        private readonly IPhysiotherapistServices _physiotherapistService;



        public SessionsController(
            ISessionServices sessionService,
            IPatientServices patientService,
            IPhysiotherapistServices physiotherapistService)
        {
            _sessionService = sessionService;
            _patientService = patientService;
            _physiotherapistService = physiotherapistService;
        }

        public async Task<IActionResult> Index()
        {
            var sessions = await _sessionService.GetAllAsync();


            foreach (var item in sessions)
            {
                Console.WriteLine("===== SESSION =====");
                Console.WriteLine($"Paciente: {item.PatientName}");
                Console.WriteLine($"Fisio: {item.PhysiotherapistName}");
                Console.WriteLine($"Cor: {item.PhysiotherapistColor}");
            }


            return View(sessions);
        }


        // GET: Admin/Sessions/Create
        public async Task<IActionResult> Create()
        {

            var model = new SessionViewModel();


            await LoadDropdowns(model);



            return View(model);

        }

        // POST: Admin/Sessions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            SessionViewModel model)
        {


            if (!ModelState.IsValid)
            {

                await LoadDropdowns(model);


                return View(model);

            }

            var result = await _sessionService
                .CreateAsync(model);

            if (!result)
            {

                ModelState.AddModelError(
                    "",
                    "Erro ao cadastrar a sessão."
                );


                await LoadDropdowns(model);


                return View(model);

            }


            TempData["Success"] =
                "Sessão cadastrada com sucesso!";


            return RedirectToAction(nameof(Index));

        }








        // GET EDIT
        public async Task<IActionResult> Edit(int id)
        {

            var session = await _sessionService.GetByIdAsync(id);


            if (session == null)
                return NotFound();



            session.Patients =
                await _patientService.GetAllPatientsAsync();


            session.Physiotherapists =
                await _physiotherapistService.GetAll();



            return View(session);

        }





        // POST EDIT

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            SessionViewModel model)
        {


            if (!ModelState.IsValid)
            {

                model.Patients =
                    await _patientService.GetAllPatientsAsync();


                model.Physiotherapists =
                    await _physiotherapistService.GetAll();


                return View(model);

            }



            await _sessionService.UpdateAsync(model);



            TempData["Success"] =
                "Sessão atualizada com sucesso!";



            return RedirectToAction(nameof(Index));

        }

        // GET DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var session = await _sessionService.GetByIdAsync(id);


            if (session == null)
                return NotFound();


            return View(session);
        }




        // POST DELETE

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int SessionId)
        {

            var result = await _sessionService.DeleteAsync(SessionId);



            if (!result)
            {
                TempData["Error"] =
                    "Não foi possível excluir a sessão.";

                return RedirectToAction(nameof(Index));
            }



            TempData["Success"] =
                "Sessão excluída com sucesso!";



            return RedirectToAction(nameof(Index));
        }


        // GET: Admin/Sessions/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var session = await _sessionService
                .GetByIdAsync(id);


            if (session == null)
            {
                return NotFound();
            }


            return View(session);
        }





        // Método auxiliar para dropdowns

        private async Task LoadDropdowns(
            SessionViewModel model)
        {


            model.Patients =
                await _patientService
                .GetAllPatientsAsync();



            model.Physiotherapists =
                await _physiotherapistService
                .GetAll();


        }

    }
}