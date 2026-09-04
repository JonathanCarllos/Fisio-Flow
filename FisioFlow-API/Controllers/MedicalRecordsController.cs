using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicalRecordsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST: api/MedicalRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetMedicalRecords()
        {
            var medicalRecords = await _unitOfWork.MedicalRecordRepository.GetAllMedicalRecordsAsync();

            return Ok(medicalRecords);
        }

        // GET: api/MedicalRecords/patientId
        [HttpGet("patient/{patientId:int}")]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetMedicalRecordsByPatientId(int patientId)
        {
            var medicalRecords = await _unitOfWork.MedicalRecordRepository.GetMedicalRecordsByPatientIdAsync(patientId);

            if (medicalRecords is null || !medicalRecords.Any())
            {
                return NotFound(new
                {
                    message = $"Nenhum registro médico encontrado para o paciente com ID {patientId}."
                });
            }
            return Ok(medicalRecords);
        }

        // POST: api/MedicalRecords
        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> CreateMedicalRecord(MedicalRecord medicalRecord)
        {
            if (medicalRecord is null)
            {
                return BadRequest(new
                {
                    message = "Os dados do registro médico são obrigatórios."
                });
            }

            await _unitOfWork.MedicalRecordRepository.AddMedicalRecordAsync(medicalRecord);

            await _unitOfWork.Commit();

            return CreatedAtAction(nameof(GetMedicalRecordsByPatientId), new { patientId = medicalRecord.PatientId }, medicalRecord);
        }

        // PUT: api/MedicalRecords/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMedicalRecord(int id, MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.MedicalRecordId)
            {
                return BadRequest(new
                {
                    message = "O ID do registro médico não corresponde ao ID fornecido."
                });
            }

            var existingMedicalRecord = await _unitOfWork.MedicalRecordRepository.GetMedicalRecordByIdAsync(id);

            if (existingMedicalRecord is null)
            {
                return NotFound(new
                {
                    message = $"Registro médico com ID {id} não encontrado."
                });
            }

            await _unitOfWork.Commit();

            return NoContent();
        }

        // DELETE: api/MedicalRecords/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMedicalRecord(int id)
        {
            try
            {
                var medicalRecord = await _unitOfWork.MedicalRecordRepository.DeleteMedicalRecordAsync(id);
              

                await _unitOfWork.Commit();

                return Ok(medicalRecord);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    message = $"MedicalRecord com ID {id} não encontrado."
                });
            }
        }
    }
}
