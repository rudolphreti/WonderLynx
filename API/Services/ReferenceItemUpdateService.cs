using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ReferenceItemUpdateService(WonderLynxContext context) : IReferenceItemUpdateService
    {
        private readonly WonderLynxContext _context = context;

        public async Task<ReferenceItem> UpdateAsync(ReferenceItem referenceItem)
        {
            _context.Entry(referenceItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return referenceItem;
        }
    }
}
