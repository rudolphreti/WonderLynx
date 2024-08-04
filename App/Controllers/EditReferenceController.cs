using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Services;
using System.Net.Http.Json;

namespace App.Controllers
{
    public class EditReferenceController(IReferenceItemUpdateService referenceItemUpdateService, IReferenceItemGetService referenceItemGetService,
        ILogger<EditReferenceController> logger) : Controller
    {
        private readonly IReferenceItemUpdateService _referenceItemUpdateService = referenceItemUpdateService;
        private readonly ILogger<EditReferenceController> _logger = logger;
        private readonly IReferenceItemGetService _referenceItemGetService = referenceItemGetService;

        public async Task<IActionResult> Edit(int id)
        {
            var referenceItem = await _referenceItemGetService.GetReferenceItem(id);

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
        public async Task<IActionResult> Edit(ReferenceItem model)
        {
            if (ModelState.IsValid)
            {
                var response = await _referenceItemUpdateService.UpdateReferenceItem(model);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Fehler beim Aktualisieren des Referenzitems. Status Code: {response.StatusCode}, Fehler: {errorContent}");
                }

                else
                {
                    _logger.LogError("Fehler beim Aktualisieren des Referenzitems.");
                    return View(model);
                }
            }
            return View(model);
        }
    }
}
