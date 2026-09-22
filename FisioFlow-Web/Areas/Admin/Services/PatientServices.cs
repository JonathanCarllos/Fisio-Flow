using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using System.Text.Json;

namespace FisioFlow_Web.Areas.Admin.Services
{
    public class PatientServices : IPatientServices
    {
        private readonly IHttpClientFactory _clientFactory;

        private const string apiEndpoint = "/api/Patients/";

        private readonly JsonSerializerOptions _options;        

        public PatientServices(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;

            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }


        public async Task<IEnumerable<PatientViewModel>> GetAllPatientsAsync()
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.GetAsync(apiEndpoint);


            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<PatientViewModel>();


            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<IEnumerable<PatientViewModel>>(stream, _options);
        }



        public async Task<PatientViewModel> GetPatientByIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.GetAsync(apiEndpoint + id);


            if (!response.IsSuccessStatusCode)
                return null;


            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<PatientViewModel>(stream, _options);
        }



        public async Task<PatientViewModel> CreatePatientAsync(PatientViewModel patientVM)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");


            using var response = await client.PostAsJsonAsync(apiEndpoint, patientVM);


            if (!response.IsSuccessStatusCode)
                return null;


            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<PatientViewModel>(stream, _options);
        }



        public async Task<PatientViewModel> UpdatePatientAsync(PatientViewModel patientVM)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");


            using var response = await client.PutAsJsonAsync(
                apiEndpoint + patientVM.PatientId,
                patientVM);


            if (!response.IsSuccessStatusCode)
                return null;


            // Caso a API retorne 204 No Content
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return patientVM;
            }


            var content = await response.Content.ReadAsStringAsync();


            if (string.IsNullOrWhiteSpace(content))
            {
                return patientVM;
            }


            return JsonSerializer.Deserialize<PatientViewModel>(
                content,
                _options
            );
        }



        public async Task<bool> DeletePatientAsync(int id)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");


            using var response = await client.DeleteAsync(apiEndpoint + id);


            return response.IsSuccessStatusCode;
        }
    }
}
