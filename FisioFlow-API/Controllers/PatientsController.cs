using AutoMapper;
using FisioFlow_API.DTOs;
using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public PatientsController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> GetPatients()
        {
            var patients = await _unitOfWork.PatientRepository
                .GetAllPatientsAsync();


            var result = _mapper.Map<IEnumerable<PatientDTO>>(patients);


            return Ok(result);
        }



        // GET: api/Patients/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PatientDTO>> GetPatient(int id)
        {
            var patient = await _unitOfWork.PatientRepository
                .GetPatientByIdAsync(id);


            if (patient is null)
            {
                return NotFound(new
                {
                    message = $"Paciente com ID {id} não encontrado."
                });
            }


            var result = _mapper.Map<PatientDTO>(patient);


            return Ok(result);
        }



        // POST: api/Patients
        [HttpPost]
        public async Task<ActionResult<PatientDTO>> CreatePatient(
            PatientDTO dto)
        {

            if (dto is null)
            {
                return BadRequest(new
                {
                    message = "Os dados do paciente são obrigatórios."
                });
            }


            var patient = _mapper.Map<Patient>(dto);


            await _unitOfWork.PatientRepository
                .AddPatientAsync(patient);


            await _unitOfWork.Commit();


            var result = _mapper.Map<PatientDTO>(patient);


            return CreatedAtAction(
                nameof(GetPatient),
                new { id = patient.PatientId },
                result
            );
        }



        // PUT: api/Patients/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePatient(
            int id,
            PatientDTO dto)
        {

            if (dto is null)
            {
                return BadRequest(new
                {
                    message = "Os dados do paciente são obrigatórios."
                });
            }


            if (id != dto.PatientId)
            {
                return BadRequest(new
                {
                    message = "O ID da URL é diferente do ID do paciente."
                });
            }



            var existingPatient = await _unitOfWork.PatientRepository
                .GetPatientByIdAsync(id);



            if (existingPatient is null)
            {
                return NotFound(new
                {
                    message = $"Paciente com ID {id} não encontrado."
                });
            }



            _mapper.Map(dto, existingPatient);



            await _unitOfWork.PatientRepository
                .UpdatePatientAsync(existingPatient);



            await _unitOfWork.Commit();



            return NoContent();
        }




        // DELETE: api/Patients/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                var patient = await _unitOfWork.PatientRepository
                    .DeletePatientAsync(id);


                await _unitOfWork.Commit();


                return Ok(new
                {
                    message = "Paciente removido com sucesso.",
                    data = _mapper.Map<PatientDTO>(patient)
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    message = $"Paciente com ID {id} não encontrado."
                });
            }
        }
    }
}