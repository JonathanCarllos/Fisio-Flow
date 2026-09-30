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
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicalRecordsController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // ============================================================
        // GET: api/MedicalRecords
        // ============================================================

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecordDTO>>> GetMedicalRecords()
        {
            var medicalRecords = await _unitOfWork
                .MedicalRecordRepository
                .GetAllMedicalRecordsAsync();

            var result = _mapper.Map<IEnumerable<MedicalRecordDTO>>(medicalRecords);

            return Ok(result);
        }

        // ============================================================
        // GET: api/MedicalRecords/patient/{patientId}
        // ============================================================

        [HttpGet("patient/{patientId:int}")]
        public async Task<ActionResult<IEnumerable<MedicalRecordDTO>>> GetMedicalRecordsByPatientId(
            int patientId)
        {
            var medicalRecords = await _unitOfWork
                .MedicalRecordRepository
                .GetMedicalRecordsByPatientIdAsync(patientId);

            if (medicalRecords == null || !medicalRecords.Any())
            {
                return NotFound(new
                {
                    message = $"Nenhum registro médico encontrado para o paciente com ID {patientId}."
                });
            }

            var result = _mapper.Map<IEnumerable<MedicalRecordDTO>>(medicalRecords);

            return Ok(result);
        }

        // ============================================================
        // GET: api/MedicalRecords/{id}
        // ============================================================

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MedicalRecordDTO>> GetMedicalRecordById(int id)
        {
            var medicalRecord = await _unitOfWork
                .MedicalRecordRepository
                .GetMedicalRecordByIdAsync(id);

            if (medicalRecord == null)
            {
                return NotFound(new
                {
                    message = $"Registro médico com ID {id} não encontrado."
                });
            }

            var result = _mapper.Map<MedicalRecordDTO>(medicalRecord);

            return Ok(result);
        }

        // ============================================================
        // POST: api/MedicalRecords
        // ============================================================

        [HttpPost]
        public async Task<ActionResult<MedicalRecordDTO>> CreateMedicalRecord(
            [FromBody] MedicalRecordDTO dto)
        {
            if (dto == null)
            {
                return BadRequest(new
                {
                    message = "Os dados do registro médico são obrigatórios."
                });
            }

            if (dto.PatientId <= 0)
            {
                return BadRequest(new
                {
                    message = "O paciente informado é inválido."
                });
            }

            if (dto.PhysiotherapistId <= 0)
            {
                return BadRequest(new
                {
                    message = "O fisioterapeuta informado é inválido."
                });
            }

            var medicalRecord = _mapper.Map<MedicalRecord>(dto);

            await _unitOfWork
                .MedicalRecordRepository
                .AddMedicalRecordAsync(medicalRecord);

            await _unitOfWork.Commit();

            // Busca novamente para carregar Patient e Physiotherapist
            var createdMedicalRecord = await _unitOfWork
                .MedicalRecordRepository
                .GetMedicalRecordByIdAsync(medicalRecord.MedicalRecordId);

            if (createdMedicalRecord == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        message = "Não foi possível recuperar o registro médico criado."
                    });
            }

            var result = _mapper.Map<MedicalRecordDTO>(createdMedicalRecord);

            return CreatedAtAction(
                nameof(GetMedicalRecordById),
                new
                {
                    id = medicalRecord.MedicalRecordId
                },
                result);
        }

        // ============================================================
        // PUT: api/MedicalRecords/{id}
        // ============================================================

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMedicalRecord(
            int id,
            [FromBody] MedicalRecordDTO dto)
        {
            if (dto == null)
            {
                return BadRequest(new
                {
                    message = "Os dados do registro médico são obrigatórios."
                });
            }

            if (id != dto.MedicalRecordId)
            {
                return BadRequest(new
                {
                    message = "O ID do registro médico não corresponde ao ID informado."
                });
            }

            if (dto.PatientId <= 0)
            {
                return BadRequest(new
                {
                    message = "O paciente informado é inválido."
                });
            }

            if (dto.PhysiotherapistId <= 0)
            {
                return BadRequest(new
                {
                    message = "O fisioterapeuta informado é inválido."
                });
            }

            var existingMedicalRecord = await _unitOfWork
                .MedicalRecordRepository
                .GetMedicalRecordByIdAsync(id);

            if (existingMedicalRecord == null)
            {
                return NotFound(new
                {
                    message = $"Registro médico com ID {id} não encontrado."
                });
            }

            // Atualiza somente os dados do registro.
            // PatientName e PhysiotherapistName são apenas campos de retorno.
            _mapper.Map(dto, existingMedicalRecord);

            await _unitOfWork
                .MedicalRecordRepository
                .UpdateMedicalRecordAsync(existingMedicalRecord);

            await _unitOfWork.Commit();

            return NoContent();
        }

        // ============================================================
        // DELETE: api/MedicalRecords/{id}
        // ============================================================

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMedicalRecord(int id)
        {
            try
            {
                var medicalRecord = await _unitOfWork
                    .MedicalRecordRepository
                    .DeleteMedicalRecordAsync(id);

                await _unitOfWork.Commit();

                return Ok(new
                {
                    message = "Registro médico removido com sucesso.",
                    data = medicalRecord
                });
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