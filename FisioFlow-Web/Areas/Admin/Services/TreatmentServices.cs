using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using System.Text.Json;

namespace FisioFlow_Web.Areas.Admin.Services
{
    public class TreatmentServices : ITreatmentServices
    {
        private readonly IHttpClientFactory _clientFactory;

        private const string apiEndpoint = "/api/Treatments/";

        private readonly JsonSerializerOptions _options;

        public TreatmentServices(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;

            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<TreatmentViewModel>> GetAllTreatmentsAsync()
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.GetAsync(apiEndpoint);


            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<TreatmentViewModel>();


            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<IEnumerable<TreatmentViewModel>>(stream, _options);
        }

        public async Task<TreatmentViewModel> GetTreatmentByIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.GetAsync(apiEndpoint + id);


            if (!response.IsSuccessStatusCode)
                return null;


            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<TreatmentViewModel>(stream, _options);
        }

        public async Task<TreatmentViewModel> CreateTreatmentAsync(TreatmentViewModel treatment)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.PostAsJsonAsync(apiEndpoint, treatment);

            if (!response.IsSuccessStatusCode)
                return null;

            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<TreatmentViewModel>(stream, _options);
        }

        public async Task<TreatmentViewModel> UpdateTreatmentAsync(TreatmentViewModel treatment)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");


            using var response = await client.PutAsJsonAsync(
                apiEndpoint + treatment.TreatmentId,
                treatment);


            if (!response.IsSuccessStatusCode)
                return null;


            // Caso a API retorne 204 No Content
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return treatment;
            }


            var content = await response.Content.ReadAsStringAsync();


            if (string.IsNullOrWhiteSpace(content))
            {
                return treatment;
            }


            return JsonSerializer.Deserialize<TreatmentViewModel>(
                content,
                _options
            );
        }

        public async Task<bool> DeleteTreatmentAsync(int id)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.DeleteAsync(apiEndpoint + id);

            return response.IsSuccessStatusCode;
        }
    }
}
