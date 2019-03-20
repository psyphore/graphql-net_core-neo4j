using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using net_core_graphql.Data.Interfaces;

namespace net_core_graphql.Helper
{
    // https://github.com/graphql-dotnet/graphql-dotnet/issues/648#issuecomment-431489339
    public class ContextServiceLocator
    {
        public IPersonRepository PersonRepository => _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IPersonRepository>();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContextServiceLocator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
