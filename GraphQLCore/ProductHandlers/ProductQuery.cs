using BusinessServices.Product;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLCore.ProductHandlers
{
    /// <summary>
    /// Actions fetch a Product
    /// </summary>
    [ExtendObjectType(Name = "Query")]
    public class ProductQuery
    {
        private readonly IProductService service;

        public ProductQuery(IProductService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Fetch a Product by their Identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductModel> ProductAsync(string id) => await service.Get(id);

        /// <summary>
        /// Fetch all Products
        /// </summary>
        /// <returns></returns>
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<ProductModel>> ProductsAsync() => await service.GetAll();
    }
}