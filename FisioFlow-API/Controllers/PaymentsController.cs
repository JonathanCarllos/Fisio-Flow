using AutoMapper;
using FisioFlow_API.DTOs;
using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public PaymentsController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        // GET: api/payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetAllPayments()
        {
            var payments = await _unitOfWork
                .PaymentRepository
                .GetAllPaymentsAsync();


            var result = _mapper.Map<IEnumerable<PaymentDTO>>(payments);


            return Ok(result);
        }



        // GET: api/payments/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PaymentDTO>> GetPaymentById(int id)
        {
            var payment = await _unitOfWork
                .PaymentRepository
                .GetPaymentByIdAsync(id);


            if (payment is null)
            {
                return NotFound(new
                {
                    message = $"Payment com ID {id} não encontrado."
                });
            }


            var result = _mapper.Map<PaymentDTO>(payment);


            return Ok(result);
        }




        // POST: api/payments
        [HttpPost]
        public async Task<ActionResult<PaymentDTO>> CreatePayment(
            PaymentDTO dto)
        {

            if (dto is null)
            {
                return BadRequest(new
                {
                    message = "Os dados do pagamento são obrigatórios."
                });
            }


            var payment = _mapper.Map<Payment>(dto);


            await _unitOfWork
                .PaymentRepository
                .CreatePaymentAsync(payment);


            await _unitOfWork.Commit();


            var result = _mapper.Map<PaymentDTO>(payment);



            return CreatedAtAction(
                nameof(GetPaymentById),
                new { id = payment.PaymentId },
                result
            );
        }




        // PUT: api/payments/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePayment(
            int id,
            PaymentDTO dto)
        {

            if (dto is null)
            {
                return BadRequest(new
                {
                    message = "Os dados do pagamento são obrigatórios."
                });
            }


            if (id != dto.PaymentId)
            {
                return BadRequest(new
                {
                    message = "O ID da URL não corresponde ao ID do pagamento."
                });
            }



            var existingPayment = await _unitOfWork
                .PaymentRepository
                .GetPaymentByIdAsync(id);



            if (existingPayment is null)
            {
                return NotFound(new
                {
                    message = $"Payment com ID {id} não encontrado."
                });
            }



            _mapper.Map(dto, existingPayment);



            await _unitOfWork
                .PaymentRepository
                .UpdatePaymentAsync(existingPayment);



            await _unitOfWork.Commit();



            return NoContent();
        }




        // DELETE: api/payments/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                var payment = await _unitOfWork
                    .PaymentRepository
                    .DeletePaymentAsync(id);


                await _unitOfWork.Commit();


                return Ok(new
                {
                    message = "Pagamento removido com sucesso.",
                    data = _mapper.Map<PaymentDTO>(payment)
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    message = $"Payment com ID {id} não encontrado."
                });
            }
        }
    }
}