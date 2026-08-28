using FisioFlow_API.Context;
using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FisioFlow_API.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PatientId == id);
        }

        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            if (patient is null)
                throw new ArgumentNullException(nameof(patient));

            await _context.Patients.AddAsync(patient);

            return patient;
        }

        public async Task<Patient> UpdatePatientAsync(Patient patient)
        {
            if (patient is null)
                throw new ArgumentNullException(nameof(patient));

            _context.Patients.Update(patient);

            return patient;
        }

        public async Task<Patient> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient is null)
                throw new KeyNotFoundException(
                    $"Paciente com ID {id} não encontrado."
                );

            _context.Patients.Remove(patient);

            return patient;
        }
    }
}