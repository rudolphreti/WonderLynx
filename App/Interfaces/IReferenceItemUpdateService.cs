using App.Models;

namespace App.Services
{
    public interface IReferenceItemUpdateService
    {
        Task<HttpResponseMessage> UpdateReferenceItem(UpdateReference model);
    }
}
