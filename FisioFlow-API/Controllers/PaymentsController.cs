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



        // ============================================================
        // GET ALL
        // ============================================================

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetAllPayments()
        {
            var payments = await _unitOfWork
                .PaymentRepository
                .GetAllPaymentsAsync();



            var result = payments.Select(payment => new PaymentDTO
            {
                PaymentId = payment.PaymentId,

                Amount = payment.Amount,

                PaymentMethod = payment.PaymentMethod,

                PaymentDate = payment.PaymentDate,

                DueDate = payment.DueDate,

                Status = payment.Status,

                Description = payment.Description,

                InsuranceName = payment.InsuranceName,


                TreatmentId = payment.TreatmentId,

                PatientId = payment.PatientId,


                PatientName = payment.Patient?.Name,

                TreatmentName = payment.Treatment?.Type

            });



            return Ok(result);
        }





        // ============================================================
        // GET BY ID
        // ============================================================

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PaymentDTO>> GetPaymentById(int id)
        {
            var payment = await _unitOfWork
                .PaymentRepository
                .GetPaymentByIdAsync(id);



            if (payment == null)
            {
                return NotFound(new
                {
                    message = "Pagamento não encontrado."
                });
            }



            var result = new PaymentDTO
            {
                PaymentId = payment.PaymentId,

                Amount = payment.Amount,

                PaymentMethod = payment.PaymentMethod,

                PaymentDate = payment.PaymentDate,

                DueDate = payment.DueDate,

                Status = payment.Status,

                Description = payment.Description,

                InsuranceName = payment.InsuranceName,


                TreatmentId = payment.TreatmentId,

                PatientId = payment.PatientId,


                PatientName = payment.Patient?.Name,

                TreatmentName = payment.Treatment?.Type
            };



            return Ok(result);
        }





        // ============================================================
        // CREATE
        // ============================================================

        [HttpPost]
        public async Task<ActionResult<PaymentDTO>> CreatePayment(
            PaymentDTO dto)
        {

            if (dto == null)
            {
                return BadRequest(new
                {
                    message = "Dados inválidos."
                });
            }



            var payment = new Payment
            {
                Amount = dto.Amount,

                PaymentMethod = dto.PaymentMethod,

                PaymentDate = dto.PaymentDate,

                DueDate = dto.DueDate,

                Status = dto.Status,

                Description = dto.Description,

                InsuranceName = dto.InsuranceName,


                PatientId = dto.PatientId,

                TreatmentId = dto.TreatmentId
            };



            await _unitOfWork
                .PaymentRepository
                .CreatePaymentAsync(payment);



            await _unitOfWork.Commit();



            var created = await _unitOfWork
                .PaymentRepository
                .GetPaymentByIdAsync(payment.PaymentId);



            var result = new PaymentDTO
            {
                PaymentId = created!.PaymentId,

                Amount = created.Amount,

                PaymentMethod = created.PaymentMethod,

                PaymentDate = created.PaymentDate,

                DueDate = created.DueDate,

                Status = created.Status,

                Description = created.Description,

                InsuranceName = created.InsuranceName,


                PatientId = created.PatientId,

                TreatmentId = created.TreatmentId,


                PatientName = created.Patient?.Name,

                TreatmentName = created.Treatment?.Type
            };



            return CreatedAtAction(
                nameof(GetPaymentById),
                new
                {
                    id = payment.PaymentId
                },
                result
            );
        }






        // ============================================================
        // UPDATE
        // ============================================================

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePayment(
            int id,
            PaymentDTO dto)
        {

            if (id != dto.PaymentId)
            {
                return BadRequest(new
                {
                    message = "ID inválido."
                });
            }



            var payment = await _unitOfWork
                .PaymentRepository
                .GetPaymentByIdAsync(id);



            if (payment == null)
            {
                return NotFound();
            }



            payment.Amount = dto.Amount;

            payment.PaymentMethod = dto.PaymentMethod;

            payment.PaymentDate = dto.PaymentDate;

            payment.DueDate = dto.DueDate;

            payment.Status = dto.Status;

            payment.Description = dto.Description;

            payment.InsuranceName = dto.InsuranceName;


            payment.PatientId = dto.PatientId;

            payment.TreatmentId = dto.TreatmentId;




            await _unitOfWork
                .PaymentRepository
                .UpdatePaymentAsync(payment);



            await _unitOfWork.Commit();



            return NoContent();
        }






        // ============================================================
        // DELETE
        // ============================================================

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePayment(int id)
        {

            var payment = await _unitOfWork
                .PaymentRepository
                .DeletePaymentAsync(id);



            if (payment == null)
            {
                return NotFound(new
                {
                    message = "Pagamento não encontrado."
                });
            }



            await _unitOfWork.Commit();



            return Ok(new
            {
                message = "Pagamento excluído com sucesso."
            });
        }

    }
}