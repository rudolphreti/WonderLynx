using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag> GetByIdAsync(int id);
        Task<Tag> AddAsync(Tag tag);
        Task<Tag> UpdateAsync(Tag tag);
        Task<bool> DeleteAsync(int id);
    }
}
