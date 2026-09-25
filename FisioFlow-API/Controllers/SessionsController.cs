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


            var result = sessions.Select(s => new SessionDTO
            {
                SessionId = s.SessionId,

                Date = s.Date,

                Time = s.Time,

                Duration = s.Duration,

                Status = s.Status,

                Notes = s.Notes,

                Evolution = s.Evolution,


                PatientId = s.PatientId,

                PatientName = s.Patient?.Name,


                PhysiotherapistId = s.PhysiotherapistId,

                PhysiotherapistName = s.Physiotherapist?.Name,

                PhysiotherapistColor = s.Physiotherapist?.Color

            });


            return Ok(result);


            return Ok(result);

        }





        // GET: api/Sessions/1

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SessionDTO>> GetSession(int id)
        {

            var session = await _unitOfWork
                .SessionRepository
                .GetSessionByIdAsync(id);



            if (session == null)
            {
                return NotFound(new
                {
                    message = "Sessão não encontrada."
                });
            }



            var result = _mapper.Map<SessionDTO>(session);



            return Ok(result);

        }





        // POST: api/Sessions

        [HttpPost]
        public async Task<ActionResult<SessionDTO>> Create(
            [FromBody] SessionDTO dto)
        {


            if (dto == null)
            {
                return BadRequest(new
                {
                    message = "Dados da sessão obrigatórios."
                });
            }



            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }





            // Verifica paciente

            var patient = await _unitOfWork
                .PatientRepository
                .GetPatientByIdAsync(dto.PatientId);



            if (patient == null)
            {
                return NotFound(new
                {
                    message = "Paciente não encontrado."
                });
            }





            // Verifica fisioterapeuta

            var physiotherapist = await _unitOfWork
                .PhysiotherapistRepository
                .GetPhysiotherapistByIdAsync(
                    dto.PhysiotherapistId
                );



            if (physiotherapist == null)
            {
                return NotFound(new
                {
                    message = "Fisioterapeuta não encontrado."
                });
            }





            // Cria somente a sessão

            var session = new Session
            {

                Date = dto.Date,

                Time = dto.Time,

                Duration = dto.Duration,

                Status = dto.Status,


                Notes = dto.Notes ?? string.Empty,


                Evolution = dto.Evolution ?? string.Empty,


                PatientId = patient.PatientId,


                PhysiotherapistId =
                    physiotherapist.PhysiotherapistId

            };






            await _unitOfWork
                .SessionRepository
                .AddSessionAsync(session);



            await _unitOfWork.Commit();






            // Busca novamente com relacionamentos

            var createdSession = await _unitOfWork
                .SessionRepository
                .GetSessionByIdAsync(
                    session.SessionId
                );



            var result = _mapper
                .Map<SessionDTO>(createdSession);




            return CreatedAtAction(
                nameof(GetSession),
                new
                {
                    id = session.SessionId
                },
                result
            );

        }






        // PUT: api/Sessions/1

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] SessionDTO dto)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var session = await _unitOfWork
                .SessionRepository
                .GetSessionByIdAsync(id);



            if (session == null)
            {
                return NotFound(new
                {
                    message = "Sessão não encontrada."
                });
            }





            session.Date = dto.Date;

            session.Time = dto.Time;

            session.Duration = dto.Duration;

            session.Status = dto.Status;


            session.Notes = dto.Notes ?? string.Empty;


            session.Evolution = dto.Evolution ?? string.Empty;


            session.PatientId = dto.PatientId;


            session.PhysiotherapistId =
                dto.PhysiotherapistId;





            await _unitOfWork
                .SessionRepository
                .UpdateSessionAsync(session);



            await _unitOfWork.Commit();




            return NoContent();

        }







        // DELETE: api/Sessions/1

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {

            var session = await _unitOfWork
                .SessionRepository
                .GetSessionByIdAsync(id);



            if (session == null)
            {
                return NotFound(new
                {
                    message = "Sessão não encontrada."
                });
            }




            await _unitOfWork
                .SessionRepository
                .DeleteSessionAsync(id);



            await _unitOfWork.Commit();



            return Ok(new
            {
                message = "Sessão removida com sucesso."
            });

        }

    }
}