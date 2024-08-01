using API;
using App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using API.Models;
using static App.Models.HomeVm;
using System.Net.Http;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient, ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7265/ReferenceItems"); //global variable https://localhost:7265/ for API URL

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var referenceItems = JsonConvert.DeserializeObject<List<ReferenceItemViewModel>>(jsonResponse);

                var viewModel = new IndexViewModel
                {
                    ReferenceItems = referenceItems ?? []
                };

                return View(viewModel);
            }
            else
            {
                // Fehlerbehandlung, falls API-Aufruf fehlschlägt
                return View(new IndexViewModel { ReferenceItems = [] });
            }
        }
    }
}
