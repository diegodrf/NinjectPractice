using Microsoft.Extensions.Caching.Memory;
using NinjectPractice.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NinjectPractice.Repositories
{
    public class ProductsRepositoryCached : IProductsRepository
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan TIME_TO_EXPIRE_CACHE = TimeSpan.FromMinutes(5);

        public ProductsRepositoryCached(IProductsRepository productsRepository, IMemoryCache memoryCache)
        {
            _productsRepository = productsRepository;
            _memoryCache = memoryCache;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            return await _productsRepository.CreateAsync(product);
        }

        public async Task<Product> UpdateAsync(int id, Product product)
        {
            return await _productsRepository.UpdateAsync(id, product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var key = "AllProducts";
            return await _memoryCache.GetOrCreateAsync(key, async options =>
            {
                options.AbsoluteExpirationRelativeToNow = TIME_TO_EXPIRE_CACHE;
                options.SetPriority(CacheItemPriority.Low);
                return await _productsRepository.GetAllAsync();
            });
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var key = $"Product{id}";
            return await _memoryCache.GetOrCreateAsync(key, async options =>
            {
                options.AbsoluteExpirationRelativeToNow = TIME_TO_EXPIRE_CACHE;
                return await _productsRepository.GetByIdAsync(id);
            });
        }


    }
}