using Microsoft.AspNetCore.Mvc;
using App.Services;
using Microsoft.Extensions.Logging;

namespace App.Controllers
{
    public class DeleteReferenceController : Controller
    {
        private readonly ReferenceItemService _referenceItemService;
        private readonly ILogger<DeleteReferenceController> _logger;

        public DeleteReferenceController(ReferenceItemService referenceItemService, ILogger<DeleteReferenceController> logger)
        {
            _referenceItemService = referenceItemService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _referenceItemService.DeleteReferenceItem(id);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Das Element wurde erfolgreich gelöscht.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                _logger.LogError("Fehler beim Löschen des Elements.");
                TempData["Message"] = "Fehler beim Löschen des Elements.";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
