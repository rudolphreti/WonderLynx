using Newtonsoft.Json;
using static App.Models.HomeVm;
using System.Net.Http;
using System.Text;

namespace App.Services
{
    public class ReferenceItemService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ReferenceItemService> _logger;

        public ReferenceItemService(IHttpClientFactory httpClientFactory, ILogger<ReferenceItemService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("API");
            _logger = logger;
        }
        public async Task<List<ReferenceItemViewModel>> GetReferenceItems()
        {
            var response = await _httpClient.GetAsync("ReferenceItems");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var referenceItems = JsonConvert.DeserializeObject<List<ReferenceItemViewModel>>(jsonResponse);
                return referenceItems;
            }
            else
            {
                _logger.LogError("Fehler beim Abrufen der Referenzitems.");
                return new List<ReferenceItemViewModel>();
            }
        }

        public async Task<ReferenceItemViewModel> GetReferenceItem(int id)
        {
            var response = await _httpClient.GetAsync($"ReferenceItems/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var referenceItem = JsonConvert.DeserializeObject<ReferenceItemViewModel>(jsonResponse);
                return referenceItem;
            }
            else
            {
                _logger.LogError("Fehler beim Abrufen des Referenzitems.");
                return null;
            }
        }

        public async Task<HttpResponseMessage> UpdateReferenceItem(ReferenceItemViewModel model)
        {
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"ReferenceItems/{model.ReferenceId}", content);
            return response;
        }

        public async Task<HttpResponseMessage> AddReferenceItem(ReferenceItemViewModel model)
        {
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("ReferenceItems", content);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteReferenceItem(int id)
        {
            var response = await _httpClient.DeleteAsync($"ReferenceItems/{id}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Fehler beim Löschen des Referenzitems mit der ID {id}.");
            }

            return response;
        }
    }
}
