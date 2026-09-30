using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using System.Net;
using System.Text.Json;

namespace FisioFlow_Web.Areas.Admin.Services
{
    public class MedicalRecordServices : IMedicalRecordServices
    {
        private readonly IHttpClientFactory _clientFactory;

        private const string apiEndpoint = "/api/MedicalRecords/";

        private readonly JsonSerializerOptions _options;

        public MedicalRecordServices(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;

            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        // ============================================================
        // GET ALL
        // ============================================================

        public async Task<IEnumerable<MedicalRecordViewModel>> GetAll()
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.GetAsync(apiEndpoint);

            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<MedicalRecordViewModel>();

            var stream = await response.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<
                IEnumerable<MedicalRecordViewModel>
            >(stream, _options);

            return result ?? Enumerable.Empty<MedicalRecordViewModel>();
        }

        // ============================================================
        // GET BY ID
        // ============================================================

        public async Task<MedicalRecordViewModel> GetById(int id)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.GetAsync(
                apiEndpoint + id
            );

            if (!response.IsSuccessStatusCode)
                return null;

            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<
                MedicalRecordViewModel
            >(stream, _options);
        }

        // ============================================================
        // CREATE
        // ============================================================

        public async Task<MedicalRecordViewModel?> CreateMedicalRecordAsync(
     MedicalRecordViewModel medicalVM)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.PostAsJsonAsync(
                apiEndpoint,
                medicalVM);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                Console.WriteLine("========================================");
                Console.WriteLine("ERRO AO CADASTRAR PRONTUÁRIO");
                Console.WriteLine($"Status: {(int)response.StatusCode}");
                Console.WriteLine($"Mensagem: {error}");
                Console.WriteLine("========================================");

                return null;
            }

            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<MedicalRecordViewModel>(
                stream,
                _options);
        }

        // ============================================================
        // UPDATE
        // ============================================================

        public async Task<MedicalRecordViewModel> UpdateMedicalRecordAsync(
            MedicalRecordViewModel medicalVM)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.PutAsJsonAsync(
                apiEndpoint + medicalVM.MedicalRecordId,
                medicalVM
            );

            if (!response.IsSuccessStatusCode)
                return null;

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return medicalVM;
            }

            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(content))
            {
                return medicalVM;
            }

            return JsonSerializer.Deserialize<MedicalRecordViewModel>(
                content,
                _options
            );
        }

        // ============================================================
        // DELETE
        // ============================================================

        public async Task<bool> DeleteMedicalRecordAsync(int id)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.DeleteAsync(
                apiEndpoint + id
            );

            return response.IsSuccessStatusCode;
        }
    }
}