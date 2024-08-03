using App.Models;

namespace App.Services
{
    public interface IReferenceItemGetService
    {
        Task<List<ReferenceItem>> GetReferenceItems();
        Task<ReferenceItem> GetReferenceItem(int id);
    }
}
