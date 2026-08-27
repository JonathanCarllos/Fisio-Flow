using FisioFlow_API.Context;
using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FisioFlow_API.Repositories
{
    public class PhysiotherapistRepository : IPhysiotherapistRepository
    {
        private readonly AppDbContext _context;

        public PhysiotherapistRepository(AppDbContext context)
        {
            _context = context;
        }   

        public async Task<IEnumerable<Physiotherapist>> GetAllPhysiotherapistsAsync()
        {
            return await _context.Physiotherapists.ToListAsync();
        }

        public async Task<Physiotherapist> GetPhysiotherapistByIdAsync(int id)
        {
            return await _context.Physiotherapists.FirstOrDefaultAsync(p => p.PhysiotherapistId == id);
        }

        public async Task<Physiotherapist> AddPhysiotherapistAsync(Physiotherapist physiotherapist)
        {
            if(physiotherapist is null)
                throw new ArgumentNullException(nameof(physiotherapist));

            await _context.Physiotherapists.AddAsync(physiotherapist);
            await _context.SaveChangesAsync();

            return physiotherapist;
        }

        public async Task<Physiotherapist> UpdatePhysiotherapistAsync(Physiotherapist physiotherapist)
        {
            if (physiotherapist is null)
                throw new ArgumentNullException(nameof(physiotherapist));

            _context.Entry(physiotherapist).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return physiotherapist;
        }

        public async Task<Physiotherapist> DeletePhysiotherapistAsync(int id)
        {
            var physiotherapist = _context.Physiotherapists.Find(id);

            if (physiotherapist is null)
                throw new ArgumentNullException($"Fisioterapeuta com ID {id} não encontrado.");

            _context.Physiotherapists.Remove(physiotherapist);

            await _context.SaveChangesAsync();

            return physiotherapist;
        }
    }
}
