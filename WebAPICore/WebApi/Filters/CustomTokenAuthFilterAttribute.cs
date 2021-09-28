using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Auth;

namespace WebApi.Filters
{
    public class CustomTokenAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        public const string TokenHeader = "TokenHeader";
        private readonly ICustomTokenManager tokenManager;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(TokenHeader, out var token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var tokenManager = context.HttpContext.RequestServices.GetService(typeof(ICustomTokenManager)) as ICustomTokenManager;

            if (tokenManager == null || !tokenManager.VerifyToken(token))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}