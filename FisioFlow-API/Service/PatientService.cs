using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;

namespace FisioFlow_API.Service
{
public class PatientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            await _unitOfWork.PatientRepository.AddPatientAsync(patient);

            await _unitOfWork.Commit();

            return patient;
        }
    }
}
