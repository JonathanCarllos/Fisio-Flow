using FisioFlow_API.Models;

namespace FisioFlow_API.Repositories.Contracts
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> GetAllSessionsAsync();
        Task<Session> GetSessionByIdAsync(int sessionId);
        Task<Session> AddSessionAsync(Session session);
        Task<Session> UpdateSessionAsync(Session session);
        Task<Session> DeleteSessionAsync(int sessionId);
    }
}
