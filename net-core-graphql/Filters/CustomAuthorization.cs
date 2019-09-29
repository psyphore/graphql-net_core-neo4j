using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Models.DTOs.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace net_core_graphql.Filters
{
    public class Auth0Requirement : IAuthorizationRequirement
    {
    }

    internal class Auth0AuthorizationHandler : AuthorizationHandler<Auth0Requirement>
    {
        private const string AUTHORIZATION_SCHEME = "Bearer ";
        private const string AUTHORIZATION_HEADER = "Authorization";
        private readonly Auth0 auth0;

        public Auth0AuthorizationHandler(Auth0 auth0)
        {
            this.auth0 = auth0;
        }

        // Check whether a given Auth0Requirement is satisfied or not for a particular context
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Auth0Requirement requirement)
        {
            try
            {
                ValidateJWT(context, requirement);
            }
            catch (Exception)
            {
                // swallow the exceptions
            }
            return Task.CompletedTask;
        }

        private void ValidateJWT(AuthorizationHandlerContext context, Auth0Requirement requirement)
        {
            if (((AuthorizationFilterContext)context.Resource).HttpContext.Request.Headers.TryGetValue(AUTHORIZATION_HEADER, out StringValues token))
            {
                var jwtToken = new JwtSecurityToken(token[0].Replace(AUTHORIZATION_SCHEME, ""));
                if (jwtToken != null && jwtToken.Issuer.Contains(auth0.Domain))
                {
                    context.Succeed(requirement);
                }
            }
        }
    }

    internal class Auth0AuthorizeAttribute : AuthorizeAttribute
    {
        private const string POLICY_PREFIX = "Auth0";

        public Auth0AuthorizeAttribute() => Age = 10;

        // Get or set the Age property by manipulating the underlying Policy property
        public int Age
        {
            get
            {
                if (int.TryParse(Policy.Substring(POLICY_PREFIX.Length), out var age))
                {
                    return age;
                }
                return default(int);
            }
            set
            {
                Policy = $"{POLICY_PREFIX}{value.ToString()}";
            }
        }
    }

    internal class Auth0PolicyProvider : IAuthorizationPolicyProvider
    {
        private const string POLICY_PREFIX = "Auth0";

        public Auth0PolicyProvider(IOptions<AuthorizationOptions> options)
        {
            // ASP.NET Core only uses one authorization policy provider, so if the custom implementation
            // doesn't handle all policies (including default policies, etc.) it should fall back to an
            // alternate provider.
            //
            // In this sample, a default authorization policy provider (constructed with options from the
            // dependency injection container) is used if this custom provider isn't able to handle a given
            // policy name.
            //
            // If a custom policy provider is able to handle all expected policy names then, of course, this
            // fallback pattern is unnecessary.
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase) &&
                int.TryParse(policyName.Substring(POLICY_PREFIX.Length), out var age))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new Auth0Requirement());
                return Task.FromResult(policy.Build());
            }

            return FallbackPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}