using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using System.Text.Json;

namespace FisioFlow_Web.Areas.Admin.Services
{
    public class ExpenseServices : IExpenseServices
    {
        private readonly IHttpClientFactory _clientFactory;

        private const string apiEndpoint = "/api/Expenses/";

        private readonly JsonSerializerOptions _options;

        public ExpenseServices(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;

            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<ExpenseViewModel>> GetAllExpensesAsync()
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.GetAsync(apiEndpoint);


            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<ExpenseViewModel>();


            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<IEnumerable<ExpenseViewModel>>(stream, _options);
        }

        public async Task<ExpenseViewModel> GetExpenseByIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.GetAsync(apiEndpoint + id);

            if (!response.IsSuccessStatusCode)
                return null;

            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<ExpenseViewModel>(stream, _options);
        }

        public async Task<ExpenseViewModel> CreateExpenseAsync(ExpenseViewModel expense)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");          

            using var response = await client.PostAsJsonAsync(apiEndpoint, expense);

            if (!response.IsSuccessStatusCode)
                return null;

            var stream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<ExpenseViewModel>(stream, _options);
        }

        public async Task<ExpenseViewModel> UpdateExpenseAsync(ExpenseViewModel expense)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");


            using var response = await client.PutAsJsonAsync(
                apiEndpoint + expense.ExpenseId,
                expense);

            if (!response.IsSuccessStatusCode)
                return null;

            // Caso a API retorne 204 No Content
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return expense;
            }

            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(content))
            {
                return expense;
            }

            return JsonSerializer.Deserialize<ExpenseViewModel>(
                content,
                _options
            );
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {
            var client = _clientFactory.CreateClient("FisioFlowAPI");

            using var response = await client.DeleteAsync(apiEndpoint + id);

            return response.IsSuccessStatusCode;
        }
    }
}