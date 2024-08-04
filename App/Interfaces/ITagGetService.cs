using App.Models;

namespace App.Services
{
    public interface ITagGetService
    {
        Task<List<Tag>> GetAllTags();
    }
}
