using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;

namespace FisioFlow_API.Service
{
    public class MedicalRecordService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicalRecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MedicalRecord> CreateMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            await _unitOfWork.MedicalRecordRepository.AddMedicalRecordAsync(medicalRecord);

            await _unitOfWork.Commit();

            return medicalRecord;
        }
    }
}
