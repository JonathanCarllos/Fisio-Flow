using AutoMapper;
using FisioFlow_API.DTOs;
using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public TreatmentsController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        // GET: api/Treatments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreatmentDTO>>> GetTreatments()
        {
            var treatments = await _unitOfWork
                .TreatmentRepository
                .GetAllTreatmentsAsync();


            var result = _mapper.Map<IEnumerable<TreatmentDTO>>(treatments);


            return Ok(result);
        }




        // GET: api/Treatments/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TreatmentDTO>> GetTreatment(int id)
        {
            var treatment = await _unitOfWork
                .TreatmentRepository
                .GetTreatmentByIdAsync(id);



            if (treatment is null)
            {
                return NotFound(new
                {
                    message = $"Tratamento com ID {id} não encontrado."
                });
            }



            var result = _mapper.Map<TreatmentDTO>(treatment);



            return Ok(result);
        }




        // POST: api/Treatments
        [HttpPost]
        public async Task<ActionResult<TreatmentDTO>> CreateTreatment(
            TreatmentDTO dto)
        {

            if (dto is null)
            {
                return BadRequest(new
                {
                    message = "Os dados do tratamento são obrigatórios."
                });
            }



            var treatment = _mapper.Map<Treatment>(dto);



            await _unitOfWork
                .TreatmentRepository
                .AddTreatmentAsync(treatment);



            await _unitOfWork.Commit();



            var result = _mapper.Map<TreatmentDTO>(treatment);



            return CreatedAtAction(
                nameof(GetTreatment),
                new { id = treatment.TreatmentId },
                result
            );
        }




        // PUT: api/Treatments/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTreatment(
            int id,
            TreatmentDTO dto)
        {

            if (dto is null)
            {
                return BadRequest(new
                {
                    message = "Os dados do tratamento são obrigatórios."
                });
            }



            if (id != dto.TreatmentId)
            {
                return BadRequest(new
                {
                    message = "O ID da URL não corresponde ao ID do tratamento."
                });
            }



            var existingTreatment = await _unitOfWork
                .TreatmentRepository
                .GetTreatmentByIdAsync(id);



            if (existingTreatment is null)
            {
                return NotFound(new
                {
                    message = $"Tratamento com ID {id} não encontrado."
                });
            }



            _mapper.Map(dto, existingTreatment);



            await _unitOfWork
                .TreatmentRepository
                .UpdateTreatmentAsync(existingTreatment);



            await _unitOfWork.Commit();



            return NoContent();
        }




        // DELETE: api/Treatments/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTreatment(int id)
        {
            try
            {
                var treatment = await _unitOfWork
                    .TreatmentRepository
                    .DeleteTreatmentAsync(id);



                await _unitOfWork.Commit();



                return Ok(new
                {
                    message = "Tratamento removido com sucesso.",
                    data = _mapper.Map<TreatmentDTO>(treatment)
                });

            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    message = $"Tratamento com ID {id} não encontrado."
                });
            }
        }
    }
}