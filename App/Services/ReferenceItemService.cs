using Newtonsoft.Json;
using App.Models;
using System.Net.Http;
using System.Text;

namespace App.Services
{
    public class ReferenceItemService(IHttpClientFactory httpClientFactory, ILogger<ReferenceItemService> logger)
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("API");
        private readonly ILogger<ReferenceItemService> _logger = logger;

        public async Task<List<ReferenceItem>> GetReferenceItems()
        {
            var response = await _httpClient.GetAsync("ReferenceItems");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var referenceItems = JsonConvert.DeserializeObject<List<ReferenceItem>>(jsonResponse);
                return referenceItems;
            }
            else
            {
                _logger.LogError("Fehler beim Abrufen der Referenzitems.");
                return [];
            }
        }

        public async Task<ReferenceItem> GetReferenceItem(int id)
        {
            var response = await _httpClient.GetAsync($"ReferenceItems/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var referenceItem = JsonConvert.DeserializeObject<ReferenceItem>(jsonResponse);
                return referenceItem;
            }
            else
            {
                _logger.LogError("Fehler beim Abrufen des Referenzitems.");
                return null;
            }
        }

        public async Task<HttpResponseMessage> UpdateReferenceItem(UpdateReference model)
        {
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"ReferenceItems/{model.ReferenceId}", content);
            return response;
        }

        public async Task<HttpResponseMessage> AddReferenceItem(AddReference model)
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

        public async Task<List<string>> GetAllTags()
        {
            var response = await _httpClient.GetAsync("Tags"); // Angenommen, der Endpunkt für Tags ist "Tags"

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tags = JsonConvert.DeserializeObject<List<string>>(jsonResponse);
                return tags;
            }
            else
            {
                _logger.LogError("Fehler beim Abrufen der Tags.");
                return [];
            }
        }

    }
}
