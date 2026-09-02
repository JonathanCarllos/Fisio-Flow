using FisioFlow_API.Context;
using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FisioFlow_API.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AppDbContext _context;

        public SessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Session>> GetAllSessionsAsync()
        {
            return await _context.Sessions
                .Include(t => t.Patient)
                .Include(t => t.Physiotherapist)
                .ToListAsync();
        }

        public async Task<Session> GetSessionByIdAsync(int sessionId)
        {
            return await _context.Sessions
                .Include(t => t.Patient)
                .Include(t => t.Physiotherapist)
                .FirstOrDefaultAsync(s => s.SessionId == sessionId);
        }

        public async Task<Session> AddSessionAsync(Session session)
        {
            if(session is null)
                throw new ArgumentNullException(nameof(session));

            await _context.Sessions.AddAsync(session);

            return session;
        }

        public async Task<Session> UpdateSessionAsync(Session session)
        {
            if(session is null)
            throw new NotImplementedException();

            _context.Sessions.Update(session);

            return session;
        }

        public async Task<Session> DeleteSessionAsync(int sessionId)
        {
            var session = await _context.Sessions.FindAsync(sessionId);

            if (session is null)
            {
                throw new KeyNotFoundException($"Session with ID {sessionId} not found.");
            }

            _context.Sessions.Remove(session);

            return session;
        }
    }
}
