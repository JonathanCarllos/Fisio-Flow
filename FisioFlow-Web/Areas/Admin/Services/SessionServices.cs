using System.Net.Http.Json;
using System.Text.Json;
using FisioFlow_Web.Areas.Admin.Models;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;
using FisioFlow_Web.Converters;

namespace FisioFlow_Web.Areas.Admin.Services
{
    public class SessionServices : ISessionServices
    {

        private readonly HttpClient _httpClient;



        public SessionServices(
            IHttpClientFactory factory)
        {
            _httpClient = factory
                .CreateClient("FisioFlowAPI");
        }





        private JsonSerializerOptions JsonOptions()
        {

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };


            options.Converters.Add(
                new TimeOnlyJsonConverter()
            );


            options.Converters.Add(
                new DateOnlyJsonConverter()
            );


            return options;

        }








        // GET ALL

        public async Task<IEnumerable<SessionViewModel>> GetAllAsync()
        {

            var response = await _httpClient
                .GetAsync("api/Sessions");



            if (!response.IsSuccessStatusCode)
            {
                return Enumerable.Empty<SessionViewModel>();
            }




            var sessions = await response.Content
                .ReadFromJsonAsync<
                    IEnumerable<SessionViewModel>
                >(
                    JsonOptions()
                );



            return sessions
                ?? Enumerable.Empty<SessionViewModel>();

        }









        // GET BY ID

        public async Task<SessionViewModel?> GetByIdAsync(
            int id)
        {

            var response = await _httpClient
                .GetAsync(
                    $"api/Sessions/{id}"
                );



            if (!response.IsSuccessStatusCode)
            {
                return null;
            }





            return await response.Content
                .ReadFromJsonAsync<SessionViewModel>(
                    JsonOptions()
                );

        }










        // CREATE

        public async Task<bool> CreateAsync(
            SessionViewModel model)
        {


            var response = await _httpClient
                .PostAsJsonAsync(
                    "api/Sessions",
                    model,
                    JsonOptions()
                );



            return response.IsSuccessStatusCode;

        }









        // UPDATE

        public async Task<bool> UpdateAsync(
            SessionViewModel model)
        {


            var response = await _httpClient
                .PutAsJsonAsync(
                    $"api/Sessions/{model.SessionId}",
                    model,
                    JsonOptions()
                );



            return response.IsSuccessStatusCode;

        }









        // DELETE

        public async Task<bool> DeleteAsync(
            int id)
        {

            var response = await _httpClient
                .DeleteAsync(
                    $"api/Sessions/{id}"
                );



            return response.IsSuccessStatusCode;

        }

    }
}