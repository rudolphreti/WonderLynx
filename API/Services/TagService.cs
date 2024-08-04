using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class TagService(WonderLynxContext context) : ITagService
    {
        private readonly WonderLynxContext _context = context;

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag> UpdateAsync(Tag tag)
        {
            _context.Entry(tag).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return false;
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateTagsForReferenceItemAsync(int referenceId, List<int> tagIds)
        {
            // Lade das ReferenceItem mit den zugehörigen Tags
            var referenceItem = await _context.ReferenceItems
                .Include(ri => ri.Tags)
                .FirstOrDefaultAsync(ri => ri.ReferenceId == referenceId);

            if (referenceItem == null)
            {
                throw new KeyNotFoundException("ReferenceItem not found");
            }

            // Entferne bestehende Tags
            referenceItem.Tags.Clear();

            // Füge neue Tags hinzu
            if (tagIds != null && tagIds.Count > 0)
            {
                var tags = await _context.Tags.Where(t => tagIds.Contains(t.TagId)).ToListAsync();
                foreach (var tag in tags)
                {
                    referenceItem.Tags.Add(tag);
                }
            }

            // Speichere die Änderungen
            await _context.SaveChangesAsync();
        }


    }
}
