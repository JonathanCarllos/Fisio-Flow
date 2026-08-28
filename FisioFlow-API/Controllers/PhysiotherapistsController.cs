using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhysiotherapistsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PhysiotherapistsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Physiotherapists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Physiotherapist>>> GetPhysiotherapists()
        {
            var physiotherapists = await _unitOfWork.PhysiotherapistRepository
                .GetAllPhysiotherapistsAsync();

            return Ok(physiotherapists);
        }

        // GET: api/Physiotherapists/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Physiotherapist>> GetPhysiotherapist(int id)
        {
            var physiotherapist = await _unitOfWork.PhysiotherapistRepository
                .GetPhysiotherapistByIdAsync(id);

            if (physiotherapist is null)
            {
                return NotFound(new
                {
                    message = $"Fisioterapeuta com ID {id} não encontrado."
                });
            }
            return Ok(physiotherapist);
        }

        // POST: api/Physiotherapists
        [HttpPost]
        public async Task<ActionResult<Physiotherapist>> CreatePhysiotherapist(Physiotherapist physiotherapist)
        {
            if (physiotherapist is null)
            {
                return BadRequest(new
                {
                    message = "Dados do fisioterapeuta não podem ser nulos."
                });
            }

            await _unitOfWork.PhysiotherapistRepository.AddPhysiotherapistAsync(physiotherapist);

            await _unitOfWork.Commit();

            return CreatedAtAction
                (nameof(GetPhysiotherapist),
                new { id = physiotherapist.PhysiotherapistId },
                physiotherapist
                );
        }

        // PUT: api/Patient/1
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Physiotherapist>> UpdatePhysiotherapist(int id,Physiotherapist physiotherapist)
        {
            if(physiotherapist is null)
            {
                return NotFound(new
                {
                    message = "Dados do fisioterapeuta inválidos."
                });
            }

            if(id != physiotherapist.PhysiotherapistId)
            {
                return BadRequest(new
                {
                    message = "ID do fisioterapeuta não corresponde ao ID fornecido."
                });
            }

            var existingPhysiotherapist = await _unitOfWork.PhysiotherapistRepository
                .GetPhysiotherapistByIdAsync(id);

            if(existingPhysiotherapist is null)
            {
                return NotFound(new
                {
                    message = $"Fisioterapeuta com ID {id} não encontrado."
                });
            }

            await _unitOfWork.PhysiotherapistRepository.UpdatePhysiotherapistAsync(physiotherapist);

            await _unitOfWork.Commit();

            return Ok(physiotherapist);
        }

        // DELETE: api/Physiotherapist/id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeletePhysiotherapist(int id)
        {
            try
            {
              var physiotherapist = await _unitOfWork.PhysiotherapistRepository
                    .DeletePhysiotherapistAsync(id);
                
                await _unitOfWork.Commit();

                return Ok(physiotherapist);

            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    message = $"Ocorreu um erro ao excluir o fisioterapeuta de Id:{id}"
                });
            }
        }
    }
}
