using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Services
{
    public interface IReferenceItemService
    {
        Task<IEnumerable<ReferenceItem>> GetAllAsync();
        Task<ReferenceItem> GetByIdAsync(int id);
        Task<ReferenceItem> AddAsync(ReferenceItem referenceItem);
        Task<ReferenceItem> UpdateAsync(ReferenceItem referenceItem);
        Task<bool> DeleteAsync(int id);
    }
}
