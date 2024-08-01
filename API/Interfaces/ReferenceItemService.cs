using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ReferenceItemService : IReferenceItemService
    {
        private readonly WonderLynxContext _context;

        public ReferenceItemService(WonderLynxContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReferenceItem>> GetAllAsync()
        {
            return await _context.ReferenceItems.ToListAsync();
        }

        public async Task<ReferenceItem> GetByIdAsync(int id)
        {
            return await _context.ReferenceItems.Include(ri => ri.Type).Include(ri => ri.Category).FirstOrDefaultAsync(ri => ri.ReferenceId == id) ?? new ReferenceItem();
        }

        public async Task<ReferenceItem> AddAsync(ReferenceItem referenceItem)
        {
            _context.ReferenceItems.Add(referenceItem);
            await _context.SaveChangesAsync();
            return referenceItem;
        }

        public async Task<ReferenceItem> UpdateAsync(ReferenceItem referenceItem)
        {
            _context.Entry(referenceItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return referenceItem;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var referenceItem = await _context.ReferenceItems.FindAsync(id);
            if (referenceItem == null)
            {
                return false;
            }

            _context.ReferenceItems.Remove(referenceItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
