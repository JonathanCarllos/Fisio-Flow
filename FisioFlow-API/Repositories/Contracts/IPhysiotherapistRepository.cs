using FisioFlow_API.Models;

namespace FisioFlow_API.Repositories.Contracts
{
    public interface IPhysiotherapistRepository
    {
        Task<IEnumerable<Physiotherapist>> GetAllPhysiotherapistsAsync();
        Task<Physiotherapist> GetPhysiotherapistByIdAsync(int id);
        Task<Physiotherapist> AddPhysiotherapistAsync(Physiotherapist physiotherapist);
        Task<Physiotherapist> UpdatePhysiotherapistAsync(Physiotherapist physiotherapist);
        Task<Physiotherapist> DeletePhysiotherapistAsync(int id);
    }
}
