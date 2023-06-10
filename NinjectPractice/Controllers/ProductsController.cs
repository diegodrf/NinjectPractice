using NinjectPractice.Models;
using NinjectPractice.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace NinjectPractice.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductsRepository _productsRepository;
        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        // GET: Products

        [HttpGet]
        public IEnumerable<Product> Index()
        {
            var products = _productsRepository.GetAll();
            return products;
        }
    }
}