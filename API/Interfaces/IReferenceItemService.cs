using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;

namespace API.Interfaces
{
    public interface IReferenceItemService
    {
        Task<IEnumerable<ReferenceItemDto>> GetAllAsync();
        Task<ReferenceItemDto> GetByIdAsync(int id);
        Task<ReferenceItem> AddAsync(ReferenceItem referenceItem);
        Task<ReferenceItem> UpdateAsync(ReferenceItem referenceItem);
        Task<bool> DeleteAsync(int id);
    }
}
