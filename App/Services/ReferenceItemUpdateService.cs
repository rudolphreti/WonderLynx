using Newtonsoft.Json;
using App.Models;
using System.Text;

namespace App.Services
{
    public class ReferenceItemUpdateService(IHttpClientFactory httpClientFactory, ILogger<ReferenceItemUpdateService> logger) : BaseService(httpClientFactory, logger), IReferenceItemUpdateService
    {
        public async Task<HttpResponseMessage> UpdateReferenceItem(ReferenceItem model)
        {
            // Update ReferenceItem details
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            _logger.LogInformation("JSON Payload for ReferenceItem: " + jsonContent);

            var response = await _httpClient.PutAsync($"ReferenceItems/{model.ReferenceId}", content);
            if (response.IsSuccessStatusCode)
            {
                // Erfolgreicher Update - hier können Sie weitere Logik hinzufügen
                _logger.LogInformation($"ReferenceItem with ID {model.ReferenceId} was successfully updated.");

                // Beispiel: Post-Update-Verarbeitung oder Benachrichtigungen
                // await NotifyUpdateSuccessAsync(model);

                return response;
            }
            else
            {
                // Fehlerhafte Antwort - hier können Sie Fehlerbehandlung hinzufügen
                _logger.LogError($"Failed to update ReferenceItem with ID {model.ReferenceId}. Status Code: {response.StatusCode}");
                return response;
            }

        }
    }
}
