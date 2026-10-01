using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IPatientServices _patientService;
    private readonly IPhysiotherapistServices _physiotherapistService;
    private readonly IExpenseServices _expenseService;

    public AdminController(
        IPatientServices patientService,
        IPhysiotherapistServices physiotherapistService,
        IExpenseServices expenseService)
    {
        _patientService = patientService;
        _physiotherapistService = physiotherapistService;
        _expenseService = expenseService;
    }

    public async Task<IActionResult> Index()
    {
        var patients = await _patientService.GetAllPatientsAsync();

        var physiotherapists = await _physiotherapistService.GetAll();

        var expenses = await _expenseService.GetAllExpensesAsync();

        var model = new AdminDashboardViewModel
        {
            TotalPatients = patients?.Count() ?? 0,

            TotalPhysiotherapists = physiotherapists?.Count() ?? 0,

            TotalExpenses = expenses?.Count() ?? 0,

            TotalExpenseAmount = expenses?.Sum(x => x.Amount) ?? 0
        };

        return View(model);
    }
}