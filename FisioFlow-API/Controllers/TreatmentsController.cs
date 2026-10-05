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






        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreatmentDTO>>> GetTreatments()
        {

            var treatments =
                await _unitOfWork
                .TreatmentRepository
                .GetAllTreatmentsAsync();



            var result =
                _mapper.Map<IEnumerable<TreatmentDTO>>(
                    treatments
                );



            return Ok(result);
        }







        [HttpGet("{id:int}")]
        public async Task<ActionResult<TreatmentDTO>> GetTreatment(
            int id)
        {

            var treatment =
                await _unitOfWork
                .TreatmentRepository
                .GetTreatmentByIdAsync(id);



            if (treatment == null)
            {
                return NotFound();
            }



            var result =
                _mapper.Map<TreatmentDTO>(
                    treatment
                );



            return Ok(result);
        }








        [HttpPost]
        public async Task<ActionResult<TreatmentDTO>> CreateTreatment(
            TreatmentDTO dto)
        {

            if (dto == null)
                return BadRequest();



            var treatment =
                _mapper.Map<Treatment>(dto);



            await _unitOfWork
                .TreatmentRepository
                .AddTreatmentAsync(treatment);



            await _unitOfWork.Commit();



            var result =
                _mapper.Map<TreatmentDTO>(
                    treatment
                );



            return CreatedAtAction(
                nameof(GetTreatment),
                new
                {
                    id = treatment.TreatmentId
                },
                result
            );
        }









        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTreatment(
            int id,
            TreatmentDTO dto)
        {

            if (dto == null)
                return BadRequest();



            if (id != dto.TreatmentId)
                return BadRequest();



            var treatment =
                await _unitOfWork
                .TreatmentRepository
                .GetTreatmentByIdAsync(id);



            if (treatment == null)
                return NotFound();





            _mapper.Map(
                dto,
                treatment
            );





            await _unitOfWork
                .TreatmentRepository
                .UpdateTreatmentAsync(
                    treatment
                );



            await _unitOfWork.Commit();



            return NoContent();
        }









        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTreatment(
            int id)
        {

            var treatment =
                await _unitOfWork
                .TreatmentRepository
                .DeleteTreatmentAsync(id);



            await _unitOfWork.Commit();



            return Ok(new
            {
                message =
                "Tratamento removido com sucesso."
            });
        }
    }
}