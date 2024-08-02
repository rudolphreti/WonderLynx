using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Services;
using static App.Models.HomeVm;

namespace App.Controllers
{
    public class AddReferenceController : Controller
    {
        private readonly ReferenceItemService _referenceItemService;
        private readonly ILogger<AddReferenceController> _logger;

        public AddReferenceController(ReferenceItemService referenceItemService, ILogger<AddReferenceController> logger)
        {
            _referenceItemService = referenceItemService;
            _logger = logger;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ReferenceItemViewModel model) //TODO: Add messages, remove redirect to home
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
