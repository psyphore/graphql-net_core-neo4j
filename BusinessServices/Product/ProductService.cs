using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessServices.Product.Extensions;
using DataAccess.CacheProvider;
using DataAccess.Product;
using Models.DTOs;

namespace BusinessServices.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;
        private readonly ICacheProvider _cache;
        private const string CACHE_KEY_PREFIX = "PRODUCT_QUERY";
        public ProductService(IProductRepository repository, ICacheProvider cache)
        {
            this.repository = repository;
            this._cache = cache;
        }

        public async Task<ProductModel> Get(string id)
        {
            var queryCacheKey = string.Format("{0}_{1}", CACHE_KEY_PREFIX, id.ToUpper());
            var found = _cache.Fetch<ProductModel>(queryCacheKey);
            if (found != null)
            {
                return found;
            }

            var results = await repository.Get(id);
            if (results != null && _cache.Save(queryCacheKey, results))
            {
                return results.ToModel();
            }

            return null;
        }

        public async Task<IEnumerable<ProductModel>> GetAll()
        {
            var results = await repository.All();
            return results.Select(r => r.ToModel());
        }
    }
}
