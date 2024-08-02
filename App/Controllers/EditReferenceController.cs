using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Services;

namespace App.Controllers
{
    public class EditReferenceController(ReferenceItemService referenceItemService, ILogger<EditReferenceController> logger) : Controller
    {
        private readonly ReferenceItemService _referenceItemService = referenceItemService;
        private readonly ILogger<EditReferenceController> _logger = logger;

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
        public async Task<IActionResult> Edit(UpdateReference model)
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
    }
}
