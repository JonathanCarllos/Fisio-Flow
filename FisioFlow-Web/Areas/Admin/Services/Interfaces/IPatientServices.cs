using FisioFlow_Web.Areas.Admin.Models;

namespace FisioFlow_Web.Areas.Admin.Services.Interfaces
{
    public interface IPatientServices
    {
        Task<IEnumerable<PatientViewModel>> GetAllPatientsAsync();
        Task<PatientViewModel> GetPatientByIdAsync(int id);
        Task<PatientViewModel> CreatePatientAsync(PatientViewModel patientVM);
        Task<PatientViewModel> UpdatePatientAsync(PatientViewModel patientVM);
        Task<bool> DeletePatientAsync(int id);
    }
}
