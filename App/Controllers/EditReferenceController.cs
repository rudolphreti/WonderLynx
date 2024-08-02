using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Services;
using static App.Models.HomeVm;

namespace App.Controllers
{
    public class EditReferenceController : Controller
    {
        private readonly ReferenceItemService _referenceItemService;
        private readonly ILogger<EditReferenceController> _logger;

        public EditReferenceController(ReferenceItemService referenceItemService, ILogger<EditReferenceController> logger)
        {
            _referenceItemService = referenceItemService;
            _logger = logger;
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
    }
}
