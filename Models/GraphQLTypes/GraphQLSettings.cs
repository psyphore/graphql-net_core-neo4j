using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Models.Types
{
    public class GraphQLSettings
    {
        public PathString Path { get; set; } = "/graphql";
        public Func<HttpContext, object> BuildUserContext { get; set; }
        public bool EnableMetrics { get; set; } = true;
        public int MaxDepth { get; set; } = 15;
        public List<object> ValidationRules { get; set; } = new List<object>();
    }
}