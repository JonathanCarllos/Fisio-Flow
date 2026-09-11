using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;

namespace FisioFlow_API.Service;

public class PaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public PaymentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Payment> CreatePaymentAsync(Payment payment)
    {
        await _unitOfWork.PaymentRepository.CreatePaymentAsync(payment);

        await _unitOfWork.Commit();
        
        return payment;
    }
}