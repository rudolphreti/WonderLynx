using Newtonsoft.Json;
using App.Models;
using System.Text;

namespace App.Services
{
    public class ReferenceItemAddService(IHttpClientFactory httpClientFactory, ILogger<ReferenceItemAddService> logger) : BaseService(httpClientFactory, logger), IReferenceItemAddService
    {
        public async Task<HttpResponseMessage> AddReferenceItem(AddReference model)
        {
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync("ReferenceItems", content);
        }
    }
}
