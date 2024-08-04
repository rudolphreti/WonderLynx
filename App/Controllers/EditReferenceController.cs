using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Services;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Controllers
{
    public class EditReferenceController(IReferenceItemUpdateService referenceItemUpdateService, IReferenceItemGetService referenceItemGetService, ITagUpdateService tagUpdateService, ITagGetService tagGetService,
        ILogger<EditReferenceController> logger) : Controller
    {
        private readonly IReferenceItemUpdateService _referenceItemUpdateService = referenceItemUpdateService;
        private readonly ILogger<EditReferenceController> _logger = logger;
        private readonly IReferenceItemGetService _referenceItemGetService = referenceItemGetService;
        private readonly ITagUpdateService _tagUpdateService = tagUpdateService;
        private readonly ITagGetService _tagGetService = tagGetService;


        public async Task<IActionResult> Edit(int id)
        {
            var referenceItem = await _referenceItemGetService.GetReferenceItem(id);
            var availableTags = await _tagGetService.GetAllTags();

            if (referenceItem != null)
            {
                ViewBag.AvailableTags = availableTags; // Tags in ViewBag speichern
                return View(referenceItem);
            }
            else
            {
                _logger.LogError("Fehler beim Abrufen des Referenzitems.");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReferenceItem model, List<int> selectedTags)
        {
            if (ModelState.IsValid)
            {
                // Update the reference item
                var refResponse = await _referenceItemUpdateService.UpdateReferenceItem(model);

                if (refResponse.IsSuccessStatusCode)
                {
                    // Update tags for the reference item
                    var tagResponse = await _tagUpdateService.UpdateTagsForReferenceAsync(model.ReferenceId, selectedTags);

                    if (tagResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Home");

                        
                    } else                     {
                        var errorContent = await tagResponse.Content.ReadAsStringAsync();
                        _logger.LogError($"Fehler beim Aktualisieren der Tags. Status Code: {tagResponse.StatusCode}, Fehler: {errorContent}");
                    }
                }
                else
                {
                    var errorContent = await refResponse.Content.ReadAsStringAsync();
                    _logger.LogError($"Fehler beim Aktualisieren des Referenzitems. Status Code: {refResponse.StatusCode}, Fehler: {errorContent}");
                }
            } else
            {
                _logger.LogError("Ungültige Eingabe beim Aktualisieren des Referenzitems.");
            }
            throw new ArgumentException("Error");
        }

    }
}
