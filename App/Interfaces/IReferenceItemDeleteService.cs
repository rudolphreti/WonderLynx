using System.Net.Http;
using System.Threading.Tasks;

namespace App.Services
{
    public interface IReferenceItemDeleteService
    {
        Task<HttpResponseMessage> DeleteReferenceItem(int id);
    }
}
