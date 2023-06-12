using NinjectPractice.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace NinjectPractice.Controllers
{
    [RoutePrefix("api/posts")]
    public class PostsController : ApiController
    {
        private readonly IJsonPlaceholderService _jsonPlaceholderService;

        public PostsController(IJsonPlaceholderService jsonPlaceholderService)
        {
            _jsonPlaceholderService = jsonPlaceholderService;
        }

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _jsonPlaceholderService.GetAllPostAsync();
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<Post> GetById(int id)
        {
            return await _jsonPlaceholderService.GetPostById(id);
        }
    }
}