using API.DTOs;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ReferenceItemGetService(WonderLynxContext context) : IReferenceItemGetService
    {
        private readonly WonderLynxContext _context = context;

        public async Task<IEnumerable<ReferenceItemDto>> GetAllAsync()
        {
            var referenceItems = await _context.ReferenceItems
                .Include(ri => ri.Type)
                .Include(ri => ri.Category)
                .Include(ri => ri.Tags)
                .ToListAsync();

            return referenceItems.Select(ri => new ReferenceItemDto
            {
                ReferenceId = ri.ReferenceId,
                Title = ri.Title ?? string.Empty,
                Subtitle = ri.Subtitle,
                TypeName = ri.Type?.Name ?? string.Empty,
                CategoryName = ri.Category?.Name ?? string.Empty,
                Description = ri.Description,
                ThumbnailUrl = ri.ThumbnailUrl,
                Tags = ri.Tags.Select(t => t.Name).ToList()
            }).ToList();
        }

        public async Task<ReferenceItemDto> GetByIdAsync(int id)
        {
            var referenceItem = await _context.ReferenceItems
                .Include(ri => ri.Type)
                .Include(ri => ri.Category)
                .Include(ri => ri.Tags)
                .FirstOrDefaultAsync(ri => ri.ReferenceId == id);

            if (referenceItem == null)
            {
                return null;
            }

            return new ReferenceItemDto
            {
                ReferenceId = referenceItem.ReferenceId,
                Title = referenceItem.Title ?? string.Empty,
                Subtitle = referenceItem.Subtitle,
                TypeName = referenceItem.Type?.Name ?? string.Empty,
                CategoryName = referenceItem.Category?.Name ?? string.Empty,
                Description = referenceItem.Description,
                ThumbnailUrl = referenceItem.ThumbnailUrl,
                Tags = referenceItem.Tags.Select(t => t.Name).ToList()
            };
        }
    }
}
