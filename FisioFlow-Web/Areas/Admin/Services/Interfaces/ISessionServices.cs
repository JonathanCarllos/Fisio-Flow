using FisioFlow_Web.Areas.Admin.Models;

namespace FisioFlow_Web.Areas.Admin.Services.Interfaces
{
    public interface ISessionServices
    {

        Task<IEnumerable<SessionViewModel>> GetAllAsync();


        Task<SessionViewModel?> GetByIdAsync(int id);


        Task<bool> CreateAsync(SessionViewModel model);


        Task<bool> UpdateAsync(SessionViewModel model);


        Task<bool> DeleteAsync(int id);

    }
}