using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Sessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetSessions()
        {
            var sessions = await _unitOfWork.SessionRepository.GetAllSessionsAsync();
            return Ok(sessions);
        }

        // GET: api/Sessions/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Session>> GetSession(int id)
        {
            var session = await _unitOfWork.SessionRepository.GetSessionByIdAsync(id);

            if (session is null)
            {
                return NotFound(new
                {
                    message = $"Sessão com ID {id} não encontrada."
                });
            }

            return Ok(session);
        }

        // POST: api/Sessions
        [HttpPost]
        public async Task<ActionResult<Session>> CreateSession(Session session)
        {
            if (session is null)
            {
                return BadRequest(new
                {
                    message = "Os dados da sessão são obrigatórios."
                });
            }
            await _unitOfWork.SessionRepository.AddSessionAsync(session);

            await _unitOfWork.Commit();

            return CreatedAtAction(nameof(GetSession), new { id = session.SessionId }, session);
        }

        // PUT: api/Sessions/id
        [HttpPost("{id:int}")]
        public async Task<ActionResult<Session>> UpdateSession(int id, Session session)
        {
            if (session is null || id != session.SessionId)
            {
                return BadRequest(new
                {
                    message = "Os dados da sessão são obrigatórios e o ID deve corresponder."
                });
            }
            var existingSession = await _unitOfWork.SessionRepository.GetSessionByIdAsync(id);

            if (existingSession is null)
            {
                return NotFound(new
                {
                    message = $"Sessão com ID {id} não encontrada."
                });
            }
            await _unitOfWork.SessionRepository.UpdateSessionAsync(session);

            await _unitOfWork.Commit();

            return Ok(session);
        }

        // DELETE: api/Sessions/id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteSession(int id)
        {
            try
            {
                var session = await _unitOfWork.SessionRepository.DeleteSessionAsync(id);               

                await _unitOfWork.Commit();

                return Ok();

            }
            catch(KeyNotFoundException)
            {
                return NotFound(new
                {
                    message = $"Session com ID {id} não encontrado."
                });
            }
        }
    }
}
