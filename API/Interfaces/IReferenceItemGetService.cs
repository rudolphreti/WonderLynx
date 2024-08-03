using API.DTOs;

namespace API.Interfaces
{
    public interface IReferenceItemGetService
    {
        Task<IEnumerable<ReferenceItemDto>> GetAllAsync();
        Task<ReferenceItemDto> GetByIdAsync(int id);
    }
}
