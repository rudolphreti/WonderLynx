using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IReferenceItemDeleteService
    {
        Task<bool> DeleteAsync(int id);
    }
}
