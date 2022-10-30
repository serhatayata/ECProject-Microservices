using Azure;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Utilities.Filters
{
    public class AuthorizeAnyPolicyFilter: IAsyncAuthorizationFilter
    {
        private readonly IAuthorizationService authorization;
        public string Policies { get; private set; }
        public AuthorizationPolicy Policy { get; }


        /// <summary>
        /// Initializes a new instance of the AuthorizeAnyPolicyFilter class.
        /// </summary>
        /// <param name="policies">A comma delimited list of policies that are allowed to access the resource.</param>
        /// <param name="authorization">The AuthorizationFilterContext.</param>
        public AuthorizeAnyPolicyFilter(string policies, IAuthorizationService authorization)
        {
            Policies = policies;
            this.authorization = authorization;
            Policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        }

        /// <summary>
        /// Called early in the filter pipeline to confirm request is authorized.
        /// </summary>
        /// <param name="context">A context for authorization filters i.e. IAuthorizationFilter and IAsyncAuthorizationFilter implementations.</param>
        /// <returns>Sets the context.Result to ForbidResult() if the user fails all of the policies listed.</returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var policies = Policies.Split(",").ToList();
            var policyEvaluator = context.HttpContext.RequestServices.GetRequiredService<IPolicyEvaluator>();
            var authenticateResult = await policyEvaluator.AuthenticateAsync(Policy, context.HttpContext);

            // Loop through policies.  User need only belong to one policy to be authorized.
            foreach (var policy in policies)
            {
                if (!authenticateResult.Succeeded)
                {
                    // Return custom 401 result
                    context.Result = new JsonResult(
                        new ErrorResult("Unauthorized. Request Access Denied", StatusCodes.Status401Unauthorized))
                    {
                        StatusCode=StatusCodes.Status401Unauthorized
                    };

                    return;
                }
                var authorized = await authorization.AuthorizeAsync(context.HttpContext.User, policy);
                if (authorized.Succeeded)
                {
                    return;
                }

            }

            context.Result = new JsonResult(
                        new ErrorResult("Forbidden. Request Access Denied", StatusCodes.Status403Forbidden))
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
            return;
        }
    }
}
