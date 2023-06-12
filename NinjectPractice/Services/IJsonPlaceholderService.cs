using System.Collections.Generic;
using System.Threading.Tasks;

namespace NinjectPractice.Services
{
    public interface IJsonPlaceholderService
    {
        Task<IEnumerable<Post>> GetAllPostAsync();
        Task<Post> GetPostById(int id);
    }
}
