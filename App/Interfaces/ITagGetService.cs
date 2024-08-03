using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Services
{
    public interface ITagGetService
    {
        Task<List<string>> GetAllTags();
    }
}
