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
    public class SessionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public SessionsController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        // GET: api/Sessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionDTO>>> GetSessions()
        {
            var sessions = await _unitOfWork
                .SessionRepository
                .GetAllSessionsAsync();


            var result = _mapper.Map<IEnumerable<SessionDTO>>(sessions);


            return Ok(result);
        }




        // GET: api/Sessions/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SessionDTO>> GetSession(int id)
        {
            var session = await _unitOfWork
                .SessionRepository
                .GetSessionByIdAsync(id);


            if (session is null)
            {
                return NotFound(new
                {
                    message = $"Sessão com ID {id} não encontrada."
                });
            }


            var result = _mapper.Map<SessionDTO>(session);


            return Ok(result);
        }




        // POST: api/Sessions
        [HttpPost]
        public async Task<ActionResult<SessionDTO>> CreateSession(
            SessionDTO dto)
        {

            if (dto is null)
            {
                return BadRequest(new
                {
                    message = "Os dados da sessão são obrigatórios."
                });
            }


            var session = _mapper.Map<Session>(dto);



            await _unitOfWork
                .SessionRepository
                .AddSessionAsync(session);



            await _unitOfWork.Commit();



            var result = _mapper.Map<SessionDTO>(session);



            return CreatedAtAction(
                nameof(GetSession),
                new { id = session.SessionId },
                result
            );
        }




        // PUT: api/Sessions/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSession(
            int id,
            SessionDTO dto)
        {

            if (dto is null)
            {
                return BadRequest(new
                {
                    message = "Os dados da sessão são obrigatórios."
                });
            }



            if (id != dto.SessionId)
            {
                return BadRequest(new
                {
                    message = "O ID da URL não corresponde ao ID da sessão."
                });
            }



            var existingSession = await _unitOfWork
                .SessionRepository
                .GetSessionByIdAsync(id);



            if (existingSession is null)
            {
                return NotFound(new
                {
                    message = $"Sessão com ID {id} não encontrada."
                });
            }



            _mapper.Map(dto, existingSession);



            await _unitOfWork
                .SessionRepository
                .UpdateSessionAsync(existingSession);



            await _unitOfWork.Commit();



            return NoContent();
        }




        // DELETE: api/Sessions/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            try
            {
                var session = await _unitOfWork
                    .SessionRepository
                    .DeleteSessionAsync(id);



                await _unitOfWork.Commit();



                return Ok(new
                {
                    message = "Sessão removida com sucesso.",
                    data = _mapper.Map<SessionDTO>(session)
                });

            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    message = $"Sessão com ID {id} não encontrada."
                });
            }
        }
    }
}