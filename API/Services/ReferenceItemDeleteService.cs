using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ReferenceItemDeleteService(WonderLynxContext context) : IReferenceItemDeleteService
    {
        private readonly WonderLynxContext _context = context;

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
