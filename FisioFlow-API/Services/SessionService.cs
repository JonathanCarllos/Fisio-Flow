using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;

namespace FisioFlow_API.Service
{
    public class SessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Session> CreateSessionAsync(Session session)
        {
            await _unitOfWork.SessionRepository.AddSessionAsync(session);
            await _unitOfWork.Commit();
            return session;
        }
    }
}
