using FisioFlow_API.Context;
using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FisioFlow_API.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Payments.Include(p => p.Treatment)
                                          .Include(p => p.Patient)
                                          .ToListAsync();
        }

        public async Task<Payment?> GetPaymentByIdAsync(int id)
        {
            return await _context.Payments.Include(p => p.Treatment)
                                          .Include(p => p.Patient)
                                          .FirstOrDefaultAsync(p => p.PaymentId == id);
        }
        public async Task<Payment?> CreatePaymentAsync(Payment payment)
        {
           if(payment is null)
                throw new ArgumentNullException(nameof(payment));

            await _context.Payments.AddAsync(payment);

            return payment;
        }

        public async Task<Payment?> UpdatePaymentAsync(Payment payment)
        {
            if(payment is null)
                throw new ArgumentNullException(nameof(payment));
            
            _context.Payments.Update(payment);
            
            return payment;
        }

        public async Task<Payment?> DeletePaymentAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment is null)
            {
                throw new KeyNotFoundException(
                        $"Payment with id {id} was not found in the database."
                    );
            }
            
            _context.Payments.Remove(payment);
            
            return payment;
        }
    }
}
