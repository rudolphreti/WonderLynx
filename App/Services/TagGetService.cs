using Newtonsoft.Json;

namespace App.Services
{
    public class TagGetService(IHttpClientFactory httpClientFactory, ILogger<TagGetService> logger) : BaseService(httpClientFactory, logger), ITagGetService
    {
        public async Task<List<string>> GetAllTags()
        {
            var response = await _httpClient.GetAsync("Tags");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<string>>(jsonResponse);
            }
            else
            {
                _logger.LogError("Fehler beim Abrufen der Tags.");
                return [];
            }
        }
    }
}
