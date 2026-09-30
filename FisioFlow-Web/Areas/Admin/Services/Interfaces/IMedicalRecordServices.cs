using FisioFlow_Web.Areas.Admin.Models;

namespace FisioFlow_Web.Areas.Admin.Services.Interfaces
{
    public interface IMedicalRecordServices
    {
        Task<IEnumerable<MedicalRecordViewModel>> GetAll();
        Task<MedicalRecordViewModel> GetById(int id);
        Task<MedicalRecordViewModel> CreateMedicalRecordAsync(MedicalRecordViewModel medicalMV);
        Task<MedicalRecordViewModel> UpdateMedicalRecordAsync(MedicalRecordViewModel medicalMV);
        Task<bool> DeleteMedicalRecordAsync(int id);
    }
}
