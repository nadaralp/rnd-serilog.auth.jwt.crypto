using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Middlewares
{
    public class AddPrincipalMiddleware
    {
        private readonly RequestDelegate _next;

        public AddPrincipalMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            HttpRequest request = context.Request;
            if (request.Query.ContainsKey("Terminate"))
            {
                await context.Response.WriteAsync("Rotue is hijacked because of terminate query");
            }

            Claim[] claims = new Claim[2];
            Claim firstClaim = new Claim("customType", "NadarAlpenidzeAuthenticated");
            claims[0] = firstClaim;

            ClaimsIdentity authorizedUserClaim = new ClaimsIdentity(claims);
            context.User.AddIdentity(authorizedUserClaim);
        }
    }
}
