using API.DTOs;
using API.Interfaces;
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

        public async Task<IEnumerable<ReferenceItemDto>> GetAllAsync()
        {
            var referenceItems = await _context.ReferenceItems
                .Include(ri => ri.Type)
                .Include(ri => ri.Category)
                .ToListAsync();

            var referenceItemDtos = referenceItems.Select(ri => new ReferenceItemDto
            {

                // I don't understand question marks here  
                ReferenceId = ri.ReferenceId,
                Title = ri.Title ?? string.Empty, 
                Subtitle = ri.Subtitle,  
                TypeName = ri.Type?.Name ?? string.Empty,  
                CategoryName = ri.Category?.Name ?? string.Empty,  
                Description = ri.Description, 
                ThumbnailUrl = ri.ThumbnailUrl
            }).ToList();

            return referenceItemDtos;
        }


        public async Task<ReferenceItemDto> GetByIdAsync(int id)
        {
            var referenceItem = await _context.ReferenceItems
                .Include(ri => ri.Type)
                .Include(ri => ri.Category)
                .FirstOrDefaultAsync(ri => ri.ReferenceId == id);

            if (referenceItem == null)
            {
                return null; // oder eine entsprechende Fehlerbehandlung
            }

            var referenceItemDto = new ReferenceItemDto
            {
                // I don't understand question marks here  
                ReferenceId = referenceItem.ReferenceId,
                Title = referenceItem.Title ?? string.Empty,
                Subtitle = referenceItem.Subtitle,
                TypeName = referenceItem.Type?.Name ?? string.Empty,
                CategoryName = referenceItem.Category?.Name ?? string.Empty,
                Description = referenceItem.Description,
                ThumbnailUrl = referenceItem.ThumbnailUrl
            };

            return referenceItemDto;
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
