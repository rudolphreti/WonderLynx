namespace App.Services
{
    public abstract class BaseService(IHttpClientFactory httpClientFactory, ILogger logger)
    {
        protected readonly HttpClient _httpClient = httpClientFactory.CreateClient("API");
        protected readonly ILogger _logger = logger;
    }
}
