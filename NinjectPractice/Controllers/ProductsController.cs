using NinjectPractice.Models;
using NinjectPractice.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace NinjectPractice.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly IProductsRepository _productsRepository;
        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productsRepository.GetAllAsync();
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<Product> GetById(int id)
        {
            return await _productsRepository.GetByIdAsync(id);
        }

        [Route("")]
        [HttpPost]
        public async Task<Product> Create([FromBody] Product product)
        {
            return await _productsRepository.CreateAsync(product);
        }

        [Route("{id:int}")]
        [HttpPut]
        public async Task<Product> Update(
            [FromUri] int id, 
            [FromBody] Product product
            )
        {
            return await _productsRepository.UpdateAsync(id, product);
        }
    }
}