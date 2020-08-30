using System.Threading.Tasks;
using BusinessServices.Product;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Models.DTOs;

namespace GraphQLCore.ProductHandlers
{
    /// <summary>
    /// Actions to create, update and delete a Product
    /// </summary>
    [ExtendObjectType(Name = "Mutation")]
    public class ProductMutation
    {
        private readonly IProductService service;
        private readonly ITopicEventSender eventSender;

        public ProductMutation(IProductService service, ITopicEventSender eventSender)
        {
            this.service = service;
            this.eventSender = eventSender;
        }

        /// <summary>
        /// Create a new Product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<ProductModel> CreateProductAsync(ProductModel model)
        {
            var payload = await Task.FromResult(new ProductModel());
            await eventSender.SendAsync(new TopicAttribute(payload.Id), "New Product Created");
            return payload;
        }

        /// <summary>
        /// Update a Product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<ProductModel> UpdateProductAsync(ProductModel model) => await Task.FromResult(new ProductModel());

        /// <summary>
        /// Delete a Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<ProductModel> RemoveProductAsync(string id) => await Task.FromResult(new ProductModel());
    }
}