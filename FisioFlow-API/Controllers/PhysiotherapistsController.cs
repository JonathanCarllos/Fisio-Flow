using AutoMapper;
using FisioFlow_API.DTOs;
using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhysiotherapistsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public PhysiotherapistsController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        // GET: api/Physiotherapists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhysiotherapistDTO>>> GetPhysiotherapists()
        {
            var physiotherapists = await _unitOfWork.PhysiotherapistRepository
                .GetAllPhysiotherapistsAsync();


            var result = _mapper.Map<IEnumerable<PhysiotherapistDTO>>(physiotherapists);


            return Ok(result);
        }



        // GET: api/Physiotherapists/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PhysiotherapistDTO>> GetPhysiotherapist(int id)
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


            var result = _mapper.Map<PhysiotherapistDTO>(physiotherapist);


            return Ok(result);
        }




        // POST: api/Physiotherapists
        [HttpPost]
        public async Task<ActionResult<PhysiotherapistDTO>> CreatePhysiotherapist(
            PhysiotherapistDTO dto)
        {

            if (dto is null)
            {
                return BadRequest(new
                {
                    message = "Dados do fisioterapeuta não podem ser nulos."
                });
            }


            var physiotherapist = _mapper.Map<Physiotherapist>(dto);



            await _unitOfWork.PhysiotherapistRepository
                .AddPhysiotherapistAsync(physiotherapist);


            await _unitOfWork.Commit();



            var result = _mapper.Map<PhysiotherapistDTO>(physiotherapist);



            return CreatedAtAction(
                nameof(GetPhysiotherapist),
                new { id = physiotherapist.PhysiotherapistId },
                result
            );
        }




        // PUT: api/Physiotherapists/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePhysiotherapist(
            int id,
            PhysiotherapistDTO dto)
        {

            if (dto is null)
            {
                return BadRequest(new
                {
                    message = "Dados do fisioterapeuta inválidos."
                });
            }



            if (id != dto.PhysiotherapistId)
            {
                return BadRequest(new
                {
                    message = "ID do fisioterapeuta não corresponde ao ID fornecido."
                });
            }



            var existingPhysiotherapist = await _unitOfWork.PhysiotherapistRepository
                .GetPhysiotherapistByIdAsync(id);



            if (existingPhysiotherapist is null)
            {
                return NotFound(new
                {
                    message = $"Fisioterapeuta com ID {id} não encontrado."
                });
            }



            _mapper.Map(dto, existingPhysiotherapist);



            await _unitOfWork.PhysiotherapistRepository
                .UpdatePhysiotherapistAsync(existingPhysiotherapist);



            await _unitOfWork.Commit();



            return NoContent();
        }




        // DELETE: api/Physiotherapists/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePhysiotherapist(int id)
        {
            try
            {
                var physiotherapist = await _unitOfWork.PhysiotherapistRepository
                    .DeletePhysiotherapistAsync(id);


                await _unitOfWork.Commit();



                return Ok(new
                {
                    message = "Fisioterapeuta removido com sucesso.",
                    data = _mapper.Map<PhysiotherapistDTO>(physiotherapist)
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    message = $"Fisioterapeuta com ID {id} não encontrado."
                });
            }
        }
    }
}