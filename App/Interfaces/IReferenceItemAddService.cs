using App.Models;

namespace App.Services
{
    public interface IReferenceItemAddService
    {
        Task<HttpResponseMessage> AddReferenceItem(AddReference model);
    }
}
