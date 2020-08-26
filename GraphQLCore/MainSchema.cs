﻿using GraphQL;
using GraphQL.Types;
using GraphQLCore.Unions;

namespace GraphQLCore
{
    public class MainSchema : Schema
    {
        public MainSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<CompositeQueries>();
            Mutation = resolver.Resolve<CompositeMutators>();
        }
    }
}