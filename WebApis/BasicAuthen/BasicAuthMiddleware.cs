using System.Net.Http.Headers;
using System.Text;

namespace WebApis.BasicAuthen
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? "");
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                var username = credentials[0];
                var password = credentials[1];

                var @const = Models.Constants.Constant;
                if (username != @const.User_Auth && password != @const.Pass_Auth)
                {
                    context.Items["MessageAuth"] = "User or Password is incorrect!";
                }
                else { 
                    context.Items["MessageAuth"] = "Success";
                }

            }
            catch
            {
                context.Items["MessageAuth"] = "";
            }

            await _next(context);
        }

    }
}
