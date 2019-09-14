using System.Security.Claims;

namespace Models.Types
{
    public class GraphQLUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}