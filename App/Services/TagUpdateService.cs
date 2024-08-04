using Newtonsoft.Json;
using System.Text;

namespace App.Services
{
    public class TagUpdateService(IHttpClientFactory httpClientFactory, ILogger<TagUpdateService> logger)
        : BaseService(httpClientFactory, logger), ITagUpdateService
    {
        public async Task<HttpResponseMessage> UpdateTagsForReferenceAsync(int referenceId, List<int> tagIds)
        {
            var requestUri = $"Tags/UpdateTagsForReference/{referenceId}";
            var content = new StringContent(JsonConvert.SerializeObject(tagIds), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestUri, content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Fehler beim Aktualisieren der Tags. Status Code: {response.StatusCode}");
            }

            return response;
        }
    }
}
