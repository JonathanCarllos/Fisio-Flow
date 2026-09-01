using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;

namespace FisioFlow_API.Service
{
    public class TreatmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TreatmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Treatment> CreateTreatmentAsync(Treatment treatment)
        {
            await _unitOfWork.TreatmentRepository.AddTreatmentAsync(treatment);

            await _unitOfWork.Commit();

            return treatment;
        }
    }
}
