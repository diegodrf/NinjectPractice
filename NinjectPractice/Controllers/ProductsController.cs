using NinjectPractice.Exceptions;
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
        public async Task<IHttpActionResult> GetById(int id)
        {
            var product = await _productsRepository.GetByIdAsync(id);
            
            if(product is null) return NotFound();

            return Ok(product);
        }

        [Route("")]
        [HttpPost]
        public async Task<Product> Create([FromBody] Product product)
        {
            return await _productsRepository.CreateAsync(product);
        }

        [Route("{id:int}")]
        [HttpPut]
        public async Task<IHttpActionResult> Update([FromUri] int id, [FromBody] Product product)
        {
            try
            {
               var newProduct =  await _productsRepository.UpdateAsync(id, product);
               return Ok(newProduct);
            }
            catch (ElementNotFound) 
            {
                return NotFound();
            }
        }
    }
}