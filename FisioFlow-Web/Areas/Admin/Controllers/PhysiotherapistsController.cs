using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PhysiotherapistsController : Controller
    {
        private readonly IPhysiotherapistServices _services;

        public PhysiotherapistsController(IPhysiotherapistServices physiotherapistServices)
        {
            _services = physiotherapistServices;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _services.GetAll();

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
        public async Task<IActionResult> Create(PhysiotherapistViewModel physiotherapistVM)
        {
            if (!ModelState.IsValid)
                return View(physiotherapistVM);

            var result = await _services.CreatePhysiotherapistAsync(physiotherapistVM);

            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Não foi possível cadastrar o fisioterapeuta.");

            return View(physiotherapistVM);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            var physiotherapist = await _services.GetPhysiotherapistByIDAsync(id);



            if (physiotherapist == null)
            {
                return NotFound();
            }

            return View(physiotherapist);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _services.GetPhysiotherapistByIDAsync(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PhysiotherapistViewModel physiotherapistVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.UpdatePhysiotherapistAsync(physiotherapistVM);

                if (result is not null)
                    return RedirectToAction(nameof(Index));
            }

            return View(physiotherapistVM);
        }

        [HttpGet]
        public async Task<ActionResult<PhysiotherapistViewModel>> Delete(int id)
        {
            var result = await _services.GetPhysiotherapistByIDAsync(id);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(
              int PhysiotherapistId)
        {
            if (PhysiotherapistId <= 0)
            {
                TempData["Error"] =
                    "Identificador do fisioterapeuta inválido.";

                return RedirectToAction(nameof(Index));
            }

            var result =
                await _services.DeletePhysiotherapistAsync(
                    PhysiotherapistId);

            if (result)
            {
                TempData["Success"] =
                    "Fisioterapeuta excluído com sucesso.";
            }
            else
            {
                TempData["Error"] =
                    "Não foi possível excluir o fisioterapeuta.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
