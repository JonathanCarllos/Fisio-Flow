using FisioFlow_Web.Areas.Admin.Models;

namespace FisioFlow_Web.Areas.Admin.Services.Interfaces
{
    public interface IPhysiotherapistServices
    {
        Task<IEnumerable<PhysiotherapistViewModel>> GetAll();
        Task<PhysiotherapistViewModel> GetPhysiotherapistByIDAsync(int id);
        Task<PhysiotherapistViewModel> CreatePhysiotherapistAsync(PhysiotherapistViewModel physiotherapistVM);
        Task<PhysiotherapistViewModel> UpdatePhysiotherapistAsync(PhysiotherapistViewModel physiotherapistVM);
        Task<bool> DeletePhysiotherapistAsync(int id);
    }
}
