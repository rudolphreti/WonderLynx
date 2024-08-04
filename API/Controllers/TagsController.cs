using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _service;

        public TagsController(ITagService service)
        {
            _service = service;
        }

        // GET: Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            var tags = await _service.GetAllAsync();
            return Ok(tags);
        }

        // GET: Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var tag = await _service.GetByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        // POST: Tags
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            var newTag = await _service.AddAsync(tag);
            return CreatedAtAction("GetTag", new { id = newTag.TagId }, newTag);
        }

        // PUT: Tags/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(int id, Tag tag)
        {
            if (id != tag.TagId)
            {
                return BadRequest();
            }

            var updatedTag = await _service.UpdateAsync(tag);
            return Ok(updatedTag);
        }

        // DELETE: Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("UpdateTagsForReference/{referenceId}")]
        public async Task<IActionResult> UpdateTagsForReference(int referenceId, [FromBody] List<int> tagIds)
        {
            try
            {
                await _service.UpdateTagsForReferenceItemAsync(referenceId, tagIds);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

    }
}
