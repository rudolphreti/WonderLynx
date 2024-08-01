
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using static App.Models.HomeVm; // question: why static?
using App.Models;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient, ILogger<HomeController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7265/ReferenceItems");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var referenceItems = JsonConvert.DeserializeObject<List<ReferenceItemViewModel>>(jsonResponse);

                var viewModel = new IndexViewModel
                {
                    ReferenceItems = referenceItems ?? new List<ReferenceItemViewModel>()
                };

                return View(viewModel);
            }
            else
            {
                _logger.LogError("Fehler beim Abrufen der Referenzitems. Statuscode: " + response.StatusCode);
                return View(new IndexViewModel { ReferenceItems = new List<ReferenceItemViewModel>() });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7265/ReferenceItems/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Das Element wurde erfolgreich gelöscht.";
            }
            else
            {
                TempData["Message"] = "Fehler beim Löschen des Elements.";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
