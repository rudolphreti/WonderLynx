using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReferenceItemsController : ControllerBase
    {
        private readonly IReferenceItemService _service;

        public ReferenceItemsController(IReferenceItemService service)
        {
            _service = service;
        }

        // GET: ReferenceItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReferenceItem>>> GetReferenceItems()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        // GET: ReferenceItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReferenceItem>> GetReferenceItem(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/ReferenceItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReferenceItem(int id, ReferenceItem referenceItem)
        {
            if (id != referenceItem.ReferenceId)
            {
                return BadRequest();
            }

            var updatedItem = await _service.UpdateAsync(referenceItem);
            return Ok(updatedItem);
        }

        // POST: api/ReferenceItems
        [HttpPost]
        public async Task<ActionResult<ReferenceItem>> PostReferenceItem(ReferenceItem referenceItem)
        {
            var newItem = await _service.AddAsync(referenceItem);
            return CreatedAtAction("GetReferenceItem", new { id = newItem.ReferenceId }, newItem);
        }

        // DELETE: api/ReferenceItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReferenceItem(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
