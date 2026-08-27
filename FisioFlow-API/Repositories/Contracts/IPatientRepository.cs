using FisioFlow_API.Models;

namespace FisioFlow_API.Repositories.Contracts
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();

        Task<Patient> GetPatientByIdAsync(int id);

        Task<Patient> AddPatientAsync(Patient patient);

        Task<Patient> UpdatePatientAsync(Patient patient);

        Task<Patient> DeletePatientAsync(int id);
    }
}