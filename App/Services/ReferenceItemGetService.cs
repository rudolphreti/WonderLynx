using Newtonsoft.Json;
using App.Models;

namespace App.Services
{
    public class ReferenceItemGetService(IHttpClientFactory httpClientFactory, ILogger<ReferenceItemGetService> logger) : BaseService(httpClientFactory, logger), IReferenceItemGetService
    {
        public async Task<List<ReferenceItem>> GetReferenceItems()
        {
            var response = await _httpClient.GetAsync("ReferenceItems");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ReferenceItem>>(jsonResponse);
            }
            else
            {
                _logger.LogError("Fehler beim Abrufen der Referenzitems.");
                return new List<ReferenceItem>();
            }
        }

        public async Task<ReferenceItem> GetReferenceItem(int id)
        {
            var response = await _httpClient.GetAsync($"ReferenceItems/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ReferenceItem>(jsonResponse);
            }
            else
            {
                _logger.LogError("Fehler beim Abrufen des Referenzitems.");
                return null;
            }
        }
    }
}
