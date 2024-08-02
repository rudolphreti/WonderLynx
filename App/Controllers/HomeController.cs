
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static App.Models.HomeVm; // question: why static?
using App.Models;
using App.Services;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ReferenceItemService _referenceItemService;

        public HomeController(ReferenceItemService referenceItemService, ILogger<HomeController> logger)
        {
            _referenceItemService = referenceItemService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var referenceItems = await _referenceItemService.GetReferenceItems();

            var viewModel = new IndexViewModel
            {
                ReferenceItems = referenceItems ?? new List<ReferenceItemViewModel>()
            };

            return View(viewModel);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ReferenceItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _referenceItemService.AddReferenceItem(model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogError("Error adding the reference item.");
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var referenceItem = await _referenceItemService.GetReferenceItem(id);

            if (referenceItem != null)
            {
                return View(referenceItem);
            }
            else
            {
                _logger.LogError("Fehler beim Abrufen des Referenzitems.");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReferenceItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _referenceItemService.UpdateReferenceItem(model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogError("Fehler beim Aktualisieren des Referenzitems.");
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _referenceItemService.DeleteReferenceItem(id);

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
