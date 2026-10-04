using FisioFlow_Web.Areas.Admin.Models;

namespace FisioFlow_Web.Areas.Admin.Services.Interfaces
{
    public interface IPaymentServices
    {
        Task<IEnumerable<PaymentViewModel>> GetAllPaymentsAsync();
        Task<PaymentViewModel> GetPaymentByIdAsync(int paymentId);
        Task<PaymentViewModel> CreatePaymentAsync(PaymentViewModel paymentVM);
        Task<PaymentViewModel> UpdatePaymentAsync(PaymentViewModel paymentVM);
        Task<bool> DeletePaymentAsync(int paymentId);
    }
}
