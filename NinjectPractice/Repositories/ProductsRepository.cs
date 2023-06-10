using NinjectPractice.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace NinjectPractice.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly AppDbContext _dbContext;
        public ProductsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var newProduct = _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
            return newProduct;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<Product> UpdateAsync(int id, Product product)
        {
            var productFromDatabase = await _dbContext.Products.FirstOrDefaultAsync(_ => _.Id == id);
            productFromDatabase.Name = product.Name;
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}