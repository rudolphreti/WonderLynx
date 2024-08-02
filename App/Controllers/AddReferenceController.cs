using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Services;

namespace App.Controllers
{
    public class AddReferenceController(ReferenceItemService referenceItemService, ILogger<AddReferenceController> logger) : Controller
    {
        private readonly ReferenceItemService _referenceItemService = referenceItemService;
        private readonly ILogger<AddReferenceController> _logger = logger;

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddReference model) //TODO: Add messages, remove redirect to home
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
    }
}
