using FisioFlow_Web.Areas.Admin.Models;

namespace FisioFlow_Web.Areas.Admin.Services.Interfaces
{
    public interface ITreatmentServices
    {
        Task<IEnumerable<TreatmentViewModel>> GetAllTreatmentsAsync();
        Task<TreatmentViewModel> GetTreatmentByIdAsync(int id);
        Task<TreatmentViewModel> CreateTreatmentAsync(TreatmentViewModel treatment);
        Task<TreatmentViewModel> UpdateTreatmentAsync(TreatmentViewModel treatment);
        Task<bool> DeleteTreatmentAsync(int id);
    }
}
