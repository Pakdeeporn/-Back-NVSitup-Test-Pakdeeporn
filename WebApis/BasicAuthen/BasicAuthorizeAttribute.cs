using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApis.BasicAuthen
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            string messageAuth = (string)(context.HttpContext.Items["MessageAuth"] ?? "");
            if (string.IsNullOrEmpty(messageAuth))
            {
                // not logged in - return 401 unauthorized
                context.Result = new JsonResult(new { access_url = "", is_success = false, message = "Unauthorized : Please input Username and Password!" }) { StatusCode = StatusCodes.Status401Unauthorized };

                // set 'WWW-Authenticate' header to trigger login popup in browsers
                context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"\", charset=\"UTF-8\"";
            }
            else if (!messageAuth.Equals("Success"))
            {
                context.Result = new JsonResult(new { access_url = "", is_success = false, message = "Unauthorized : " + messageAuth }) { StatusCode = StatusCodes.Status401Unauthorized };

            }
        }

    }
}
