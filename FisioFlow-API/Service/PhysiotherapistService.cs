using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;

namespace FisioFlow_API.Service
{
    public class PhysiotherapistService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PhysiotherapistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Physiotherapist> CreatePhysiotherapistAsync(Physiotherapist physiotherapist)
        {
            await _unitOfWork.PhysiotherapistRepository.AddPhysiotherapistAsync(physiotherapist);

            await _unitOfWork.Commit();

            return physiotherapist;
        }
    }
}
