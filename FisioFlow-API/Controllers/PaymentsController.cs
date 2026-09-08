using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Get: api/payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAllPayments()
        {
            var payments = await _unitOfWork.PaymentRepository.GetAllPaymentsAsync();

            return Ok(payments);
        }

        //Get: api/payments/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Payment>> GetPaymentById(int id)
        {
            var payment = await _unitOfWork.PaymentRepository.GetPaymentByIdAsync(id);

            if (payment is null)
            {
                return NotFound(new
                {
                    message = $"Payment com ID {id} não encontrado."
                });
            }

            return Ok(payment);
        }

        // POST: api/payments
        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            if (payment is null)
            {
                return BadRequest(new
                {
                    message = "Os dados do payment são obrigatórios."
                });
            }

            await _unitOfWork.PaymentRepository.CreatePaymentAsync(payment);

            await _unitOfWork.Commit();

            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.PaymentId }, payment);
        }

        // PUT: api/payments/id
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Payment>> UpdatePayment(int id, Payment payment)
        {
            if (payment is null)
            {
                return BadRequest(new
                {
                    message = "Os dados do payment são obrigatórios e o ID deve corresponder."
                });
            }

            if(id != payment.PaymentId)
            {
                return BadRequest(new
                {
                    message = "O ID do payment na URL não corresponde ao ID no corpo da solicitação."
                });
            }

            var existingPayment = await _unitOfWork.PaymentRepository.GetPaymentByIdAsync(id);

            if (existingPayment is null)
            {
                return NotFound(new
                {
                    message = $"Payment com ID {id} não encontrado."
                });
            }

            await _unitOfWork.PaymentRepository.UpdatePaymentAsync(payment);

            await _unitOfWork.Commit();

            return NoContent();
        }

        // DELETE: api/payments/id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletePayment(int id)
        {
            try
            {
                var payment = await _unitOfWork.PaymentRepository.DeletePaymentAsync(id);

                await _unitOfWork.Commit();

                return Ok(payment);
            }
            catch(KeyNotFoundException)
            {
                return NotFound(new
                {
                    message = $"Payment com ID {id} não encontrado."
                });
            }            
        }

    }
}
