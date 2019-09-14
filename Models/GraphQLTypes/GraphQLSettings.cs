using Microsoft.AspNetCore.Http;
using System;

namespace Models.Types
{
    public class GraphQLSettings
    {
        public PathString Path { get; set; } = "/graphql";
        public Func<HttpContext, object> BuildUserContext { get; set; }
        public bool EnableMetrics { get; set; }
        public int MaxDepth { get; set; } = 15;
    }
}