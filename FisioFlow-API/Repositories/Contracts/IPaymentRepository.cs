using FisioFlow_API.Models;

namespace FisioFlow_API.Repositories.Contracts
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task<Payment?> CreatePaymentAsync(Payment payment);
        Task<Payment?> UpdatePaymentAsync(Payment payment);
        Task<Payment?> DeletePaymentAsync(int id);
    }
}
