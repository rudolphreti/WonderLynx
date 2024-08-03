using API.Models;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IReferenceItemAddService
    {
        Task<ReferenceItem> AddAsync(ReferenceItem referenceItem);
    }
}
