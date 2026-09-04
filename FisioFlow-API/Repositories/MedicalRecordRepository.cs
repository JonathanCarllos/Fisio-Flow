using FisioFlow_API.Context;
using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FisioFlow_API.Repositories
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly AppDbContext _context;

        public MedicalRecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MedicalRecord>> GetAllMedicalRecordsAsync()
        {
            return await _context.MedicalRecords.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecordsByPatientIdAsync(int patientId)
        {
            return await _context.MedicalRecords
                .Where(mr => mr.PatientId == patientId)
                .AsNoTracking()
                .ToListAsync();
        }      

        public async Task<MedicalRecord> GetMedicalRecordByIdAsync(int id)
        {
            return await _context.MedicalRecords
                .AsNoTracking()
                .FirstOrDefaultAsync(mr => mr.MedicalRecordId == id);
        }

        public async Task<MedicalRecord> AddMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            if(medicalRecord is null)
                throw new ArgumentNullException(nameof(medicalRecord));

            await _context.MedicalRecords.AddAsync(medicalRecord);

            return medicalRecord;
        }

        public async Task<MedicalRecord> UpdateMedicalRecordAsync(MedicalRecord medicalRecord)
        {
            if(medicalRecord is null)
                throw new ArgumentNullException(nameof(medicalRecord));

            _context.MedicalRecords.Update(medicalRecord);
            return medicalRecord;
        }

        public async Task<MedicalRecord> DeleteMedicalRecordAsync(int id)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);

            if (medicalRecord is null)
                throw new KeyNotFoundException($"Medical record with ID {id} not found.");

            _context.MedicalRecords.Remove(medicalRecord);

            return medicalRecord;
        }

    }
}
