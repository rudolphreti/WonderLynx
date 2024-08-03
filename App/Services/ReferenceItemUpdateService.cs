﻿using Newtonsoft.Json;
using App.Models;
using System.Text;

namespace App.Services
{
    public class ReferenceItemUpdateService(IHttpClientFactory httpClientFactory, ILogger<ReferenceItemUpdateService> logger) : BaseService(httpClientFactory, logger), IReferenceItemUpdateService
    {
        public async Task<HttpResponseMessage> UpdateReferenceItem(UpdateReference model)
        {
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            return await _httpClient.PutAsync($"ReferenceItems/{model.ReferenceId}", content);
        }
    }
}
