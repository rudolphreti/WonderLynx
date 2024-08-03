using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ReferenceItemAddService(WonderLynxContext context) : IReferenceItemAddService
    {
        private readonly WonderLynxContext _context = context;

        public async Task<ReferenceItem> AddAsync(ReferenceItem referenceItem)
        {
            _context.ReferenceItems.Add(referenceItem);
            await _context.SaveChangesAsync();
            return referenceItem;
        }
    }
}
