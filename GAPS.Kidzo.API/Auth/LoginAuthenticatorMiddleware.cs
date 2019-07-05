using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Threading.Tasks;

namespace HelloParent.Auth
{
    public class LoginAuthenticatorMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginAuthenticatorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Microsoft.Extensions.Primitives.StringValues v2LoginCookieValue = context.Request.Headers["v2Login"];

            if (!string.IsNullOrWhiteSpace(Convert.ToString(v2LoginCookieValue)))
            {
                string userId = Base64Decode(v2LoginCookieValue);

                if (context.User?.Identities == null)
                {
                    GenericIdentity identity = new GenericIdentity("userid", userId);
                    context.User.AddIdentity(identity);
                }
                //context.User.AddIdentity()

                JwtSecurityToken tokenOptions = new JwtSecurityToken(claims: context.User.Claims);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            }

            await _next(context);

            //anything here is i.e. after next is called, will be invoked while sending response back.
        }

        private string Base64Decode(string base64EncodedData)
        {
            byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
