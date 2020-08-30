using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using Models.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLCore.ProductHandlers
{
    [ExtendObjectType(Name = "Subscription")]
    public class ProductSubscription
    {
        [SubscribeAndResolve]
        public async Task<IAsyncEnumerable<ProductModel>> OnMessageReceivedAsync(
            [GlobalState] string currentUserEmail,
            [Service] ITopicEventReceiver eventReceiver,
            CancellationToken cancellationToken)
        {
            return await eventReceiver.SubscribeAsync<string, ProductModel>(
                    currentUserEmail, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
