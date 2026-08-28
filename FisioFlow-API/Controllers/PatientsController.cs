using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Patient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            var patients = await _unitOfWork.PatientRepository
                .GetAllPatientsAsync();

            return Ok(patients);
        }

        // GET: api/Patient/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
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

            return Ok(patient);
        }

        // POST: api/Patient
        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            if (patient is null)
            {
                return BadRequest(new
                {
                    message = "Os dados do paciente são obrigatórios."
                });
            }

            await _unitOfWork.PatientRepository
                .AddPatientAsync(patient);

            await _unitOfWork.Commit();

            return CreatedAtAction(
                nameof(GetPatient),
                new { id = patient.PatientId },
                patient
            );
        }

        // PUT: api/Patient/1
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Patient>> UpdatePatient(
            int id,
            Patient patient)
        {
            if (patient is null)
            {
                return BadRequest(new
                {
                    message = "Os dados do paciente são obrigatórios."
                });
            }

            if (id != patient.PatientId)
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

            await _unitOfWork.PatientRepository
                .UpdatePatientAsync(patient);

            await _unitOfWork.Commit();

            return Ok(patient);
        }

        // DELETE: api/Patient/1
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
            try
            {
                var patient = await _unitOfWork.PatientRepository
                    .DeletePatientAsync(id);

                await _unitOfWork.Commit();

                return Ok(patient);
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

