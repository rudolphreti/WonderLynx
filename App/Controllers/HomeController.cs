
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using App.Models;
using App.Services;

namespace App.Controllers
{
    public class HomeController(IReferenceItemGetService referenceItemGetService, ILogger<HomeController> logger) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IReferenceItemGetService _referenceItemGetService = referenceItemGetService;

        public async Task<IActionResult> Index()
        {
            var referenceItems = await _referenceItemGetService.GetReferenceItems();

            HomeVm viewModel = new()
            {
                ReferenceItems = referenceItems
            };

            return View(viewModel);
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
