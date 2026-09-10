using AutoMapper;
using FisioFlow_API.DTOs;
using FisioFlow_API.Models;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FisioFlow_API.Controllers
{
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


        // GET: api/MedicalRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecordDTO>>> GetMedicalRecords()
        {
            var medicalRecords = await _unitOfWork
                .MedicalRecordRepository
                .GetAllMedicalRecordsAsync();


            var result = _mapper.Map<IEnumerable<MedicalRecordDTO>>(medicalRecords);

            return Ok(result);
        }


        // GET: api/MedicalRecords/patient/{patientId}
        [HttpGet("patient/{patientId:int}")]
        public async Task<ActionResult<IEnumerable<MedicalRecordDTO>>> GetMedicalRecordsByPatientId(int patientId)
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



        // POST: api/MedicalRecords
        [HttpPost]
        public async Task<ActionResult<MedicalRecordDTO>> CreateMedicalRecord(
            MedicalRecordDTO dto)
        {

            if (dto == null)
            {
                return BadRequest(new
                {
                    message = "Os dados do registro médico são obrigatórios."
                });
            }


            var medicalRecord = _mapper.Map<MedicalRecord>(dto);


            await _unitOfWork
                .MedicalRecordRepository
                .AddMedicalRecordAsync(medicalRecord);


            await _unitOfWork.Commit();


            var result = _mapper.Map<MedicalRecordDTO>(medicalRecord);


            return CreatedAtAction(
                nameof(GetMedicalRecordsByPatientId),
                new { patientId = medicalRecord.PatientId },
                result);
        }



        // PUT: api/MedicalRecords/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMedicalRecord(
            int id,
            MedicalRecordDTO dto)
        {

            if (id != dto.MedicalRecordId)
            {
                return BadRequest(new
                {
                    message = "O ID do registro médico não corresponde ao ID informado."
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


            _mapper.Map(dto, existingMedicalRecord);


            await _unitOfWork.Commit();


            return NoContent();
        }



        // DELETE: api/MedicalRecords/{id}
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