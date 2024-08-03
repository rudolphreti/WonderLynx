namespace App.Services
{
    public class ReferenceItemDeleteService(IHttpClientFactory httpClientFactory, ILogger<ReferenceItemDeleteService> logger) : BaseService(httpClientFactory, logger), IReferenceItemDeleteService
    {
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
