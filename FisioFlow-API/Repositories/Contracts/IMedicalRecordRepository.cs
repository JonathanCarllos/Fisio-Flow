using FisioFlow_API.Models;

namespace FisioFlow_API.Repositories.Contracts
{
    public interface IMedicalRecordRepository
    {
        Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync();
        Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPatientIdAsync(int patientId);      
        Task<MedicalRecord> GetMedicalRecordByIdAsync(int id);
        Task<MedicalRecord> AddMedicalRecordAsync(MedicalRecord medicalRecord);
        Task<MedicalRecord> UpdateMedicalRecordAsync(MedicalRecord medicalRecord);
        Task<MedicalRecord> DeleteMedicalRecordAsync(int id);
    }
}
