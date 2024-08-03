using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReferenceItemsController(
        IReferenceItemGetService getService,
        IReferenceItemAddService addService,
        IReferenceItemUpdateService updateService,
        IReferenceItemDeleteService deleteService) : ControllerBase
    {
        private readonly IReferenceItemGetService _getService = getService;
        private readonly IReferenceItemAddService _addService = addService;
        private readonly IReferenceItemUpdateService _updateService = updateService;
        private readonly IReferenceItemDeleteService _deleteService = deleteService;

        // GET: ReferenceItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReferenceItem>>> GetReferenceItems()
        {
            var items = await _getService.GetAllAsync();
            return Ok(items);
        }

        // GET: ReferenceItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReferenceItem>> GetReferenceItem(int id)
        {
            var item = await _getService.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: ReferenceItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReferenceItem(int id, ReferenceItem referenceItem)
        {
            if (id != referenceItem.ReferenceId)
            {
                return BadRequest();
            }

            var updatedItem = await _updateService.UpdateAsync(referenceItem);
            return Ok(updatedItem);
        }

        // POST: ReferenceItems
        [HttpPost]
        public async Task<ActionResult<ReferenceItem>> PostReferenceItem(ReferenceItem referenceItem)
        {
            var newItem = await _addService.AddAsync(referenceItem);
            return CreatedAtAction("GetReferenceItem", new { id = newItem.ReferenceId }, newItem);
        }

        // DELETE: ReferenceItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReferenceItem(int id)
        {
            var result = await _deleteService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
