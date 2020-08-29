using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace GraphQLCore
{
    public class GraphQLSettings
    {
        public PathString Path { get; set; } = "/api/graphql";
        public Func<HttpContext, IDictionary<string, object>> BuildUserContext { get; set; }
        public bool EnableMetrics { get; set; }
        public int MaxDepth { get; set; } = 7;
        public List<object> ValidationRules { get; set; } = new List<object>();
    }
}
