using FisioFlow_API.Models;

namespace FisioFlow_API.Repositories.Contracts
{
    public interface ITreatment
    {
        Task<IEnumerable<Treatment>> GetAllTreatmentsAsync();
        Task<Treatment> GetTreatmentByIdAsync(int id);
        Task<Treatment> AddTreatmentAsync(Treatment treatment);
        Task<Treatment> UpdateTreatmentAsync(Treatment treatment);
        Task<Treatment> DeleteTreatmentAsync(int id);
    }
}
