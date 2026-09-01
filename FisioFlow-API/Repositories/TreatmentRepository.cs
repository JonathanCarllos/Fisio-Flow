using FisioFlow_API.Context;
using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FisioFlow_API.Repositories
{
    public class TreatmentRepository : ITreatment
    {
        private readonly AppDbContext _context;

        public TreatmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Treatment>> GetAllTreatmentsAsync()
        {
            return await _context.Treatments
                .Include(t => t.Patient)
                .Include(t => t.Physiotherapist)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Treatment> GetTreatmentByIdAsync(int id)
        {
           return await _context.Treatments
                .Include(t => t.Patient)
                .Include(t => t.Physiotherapist)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TreatmentId == id);
        }

        public async Task<Treatment> AddTreatmentAsync(Treatment treatment)
        {
            if(treatment is null)
                throw new ArgumentNullException(nameof(treatment));

            await _context.Treatments.AddAsync(treatment);

            return treatment;
        }

        public async Task<Treatment> UpdateTreatmentAsync(Treatment treatment)
        {
            if (treatment is null)
                throw new ArgumentNullException(nameof(treatment));

            _context.Treatments.Update(treatment);

            return treatment;
        }

        public async Task<Treatment> DeleteTreatmentAsync(int id)
        {
            var treatment = await _context.Treatments.FindAsync(id);

            if (treatment is null)
            {
                throw new KeyNotFoundException($"Tratamento ID {id} não encontrado.");
            }

            _context.Treatments.Remove(treatment);

            return treatment;
        }
    }
}
