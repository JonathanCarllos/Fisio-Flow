using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using System.Text.Json;

namespace FisioFlow_Web.Areas.Admin.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IHttpClientFactory _clientFactory;

        private const string apiEndpoint = "/api/Payments/";

        private readonly JsonSerializerOptions _options;

        public PaymentServices(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;

            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<PaymentViewModel>> GetAllPaymentsAsync()
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.GetAsync(apiEndpoint);

            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<PaymentViewModel>();


            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<IEnumerable<PaymentViewModel>>(
                stream,
                _options
            );
        }
        public async Task<PaymentViewModel> GetPaymentByIdAsync(int paymentId)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.GetAsync(apiEndpoint + paymentId);

            if (!response.IsSuccessStatusCode)
                return null;

            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<PaymentViewModel>(stream, _options);
        }

        public async Task<PaymentViewModel> CreatePaymentAsync(PaymentViewModel paymentVM)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.PostAsJsonAsync(apiEndpoint, paymentVM);

            if (!response.IsSuccessStatusCode)
                return null;

            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<PaymentViewModel>(stream, _options);
        }

        public async Task<PaymentViewModel> UpdatePaymentAsync(PaymentViewModel paymentVM)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");


            using var response = await client.PutAsJsonAsync(
                apiEndpoint + paymentVM.PaymentId,
                paymentVM);

            if (!response.IsSuccessStatusCode)
                return null;

            // Caso a API retorne 204 No Content
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return paymentVM;
            }

            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(content))
            {
                return paymentVM;
            }

            return JsonSerializer.Deserialize<PaymentViewModel>(
                content,
                _options
            );
        }

        public async Task<bool> DeletePaymentAsync(int paymentId)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.DeleteAsync(apiEndpoint + paymentId);

            return response.IsSuccessStatusCode;
        }
    }
}
