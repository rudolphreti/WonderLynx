using API.Models;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IReferenceItemUpdateService
    {
        Task<ReferenceItem> UpdateAsync(ReferenceItem referenceItem);
    }
}
