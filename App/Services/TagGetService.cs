using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using App.Models; // Stellen Sie sicher, dass dieser Import vorhanden ist

namespace App.Services
{
    public class TagGetService(IHttpClientFactory httpClientFactory, ILogger<TagGetService> logger) : BaseService(httpClientFactory, logger), ITagGetService
    {
        public async Task<List<Tag>> GetAllTags()
        {
            var response = await _httpClient.GetAsync("Tags");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Tag>>(jsonResponse);
            }
            else
            {
                _logger.LogError("Fehler beim Abrufen der Tags.");
                return []; // Zurückgeben einer leeren Liste, wenn es Fehler gibt.
            }
        }
    }
}
