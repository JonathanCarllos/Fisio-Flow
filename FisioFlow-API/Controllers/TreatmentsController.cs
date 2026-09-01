using FisioFlow_API.Models;
using FisioFlow_API.Repositories;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreatmentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TreatmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Treatments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Treatment>>> GetTreatments()
        {
            var treatments = await _unitOfWork.TreatmentRepository.GetAllTreatmentsAsync();
            return Ok(treatments);
        }

        // GET: api/Treatments/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Treatment>> GetTreatment(int id)
        {
            var treatment = await _unitOfWork.TreatmentRepository.GetTreatmentByIdAsync(id);
            if (treatment is null)
            {
                return NotFound(new
                {
                    message = $"Tratamento com ID {id} não encontrado."
                });
            }
            return Ok(treatment);
        }

        // POST: api/Treatments
        [HttpPost]
        public async Task<ActionResult<Treatment>> CreateTreatment(Treatment treatment)
        {
            if (treatment is null)
            {
                return BadRequest(new
                {
                    message = "Os dados do tratamento são obrigatórios."
                });
            }
            await _unitOfWork.TreatmentRepository.AddTreatmentAsync(treatment);

            await _unitOfWork.Commit();

            return CreatedAtAction(nameof(GetTreatment), new { id = treatment.TreatmentId }, treatment);
        }

        // PUT: api/Treatments/id
        [HttpPost("{id:int}")]
        public async Task<ActionResult<Treatment>> UpdateTreatment(int id, Treatment treatment)
        {
            if (treatment is null || id != treatment.TreatmentId)
            {
                return BadRequest(new
                {
                    message = "Os dados do tratamento são obrigatórios e o ID deve corresponder."
                });
            }

            var existingTreatment = await _unitOfWork.TreatmentRepository.GetTreatmentByIdAsync(id);

            if (existingTreatment is null)
            {
                return NotFound(new
                {
                    message = $"Tratamento com ID {id} não encontrado."
                });
            }

            await _unitOfWork.TreatmentRepository.UpdateTreatmentAsync(treatment);

            await _unitOfWork.Commit();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTreatment(int id)
        {
            try
            {
                var treatment = await _unitOfWork.TreatmentRepository.DeleteTreatmentAsync(id);

                await _unitOfWork.Commit();

                return Ok(treatment);

            }
            catch(KeyNotFoundException)
            {
                return NotFound(new
                {
                    message = $"Treament com ID {id} não encontrado."
                });
            }
        }
    }
}
