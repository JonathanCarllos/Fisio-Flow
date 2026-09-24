using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using System.Text.Json;

namespace FisioFlow_Web.Areas.Admin.Services
{
    public class PhysiotherapistServices : IPhysiotherapistServices
    {
        private readonly IHttpClientFactory _clientFactory;

        private const string apiEndpoint = "/api/Physiotherapists/";

        private readonly JsonSerializerOptions _options;

        public PhysiotherapistServices(IHttpClientFactory client)
        {
            _clientFactory = client;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<PhysiotherapistViewModel>> GetAll()
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.GetAsync(apiEndpoint);

            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<PhysiotherapistViewModel>();

            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<IEnumerable<PhysiotherapistViewModel>>(stream, _options);

        }

        public async Task<PhysiotherapistViewModel> GetPhysiotherapistByIDAsync(int id)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.GetAsync(apiEndpoint + id);

            if (!response.IsSuccessStatusCode)
                return null;


            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<PhysiotherapistViewModel>(stream, _options);
        }

        public async Task<PhysiotherapistViewModel> CreatePhysiotherapistAsync(PhysiotherapistViewModel physiotherapistVM)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.PostAsJsonAsync(apiEndpoint, physiotherapistVM);

            if (!response.IsSuccessStatusCode)
                return null;

            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<PhysiotherapistViewModel>(stream, _options);
        }

        public async Task<PhysiotherapistViewModel> UpdatePhysiotherapistAsync(PhysiotherapistViewModel physiotherapistVM)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.PutAsJsonAsync(
               apiEndpoint + physiotherapistVM.PhysiotherapistId,
               physiotherapistVM);

            if (!response.IsSuccessStatusCode)
                return null;


            // Caso a API retorne 204 No Content
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return physiotherapistVM;
            }


            var content = await response.Content.ReadAsStringAsync();


            if (string.IsNullOrWhiteSpace(content))
            {
                return physiotherapistVM;
            }


            return JsonSerializer.Deserialize<PhysiotherapistViewModel>(
                content,
                _options
            );
        }

        public async Task<bool> DeletePhysiotherapistAsync(int id)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.DeleteAsync(apiEndpoint + id);


            return response.IsSuccessStatusCode;
        }
    }
}
