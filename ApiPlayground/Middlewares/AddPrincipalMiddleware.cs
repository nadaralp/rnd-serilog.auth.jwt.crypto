using ApiPlayground.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiPlayground.Middlewares
{
    public class AddPrincipalMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AddPrincipalMiddleware> _logger;

        public AddPrincipalMiddleware(RequestDelegate next, ILogger<AddPrincipalMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogDebug($"Inside {nameof(AddPrincipalMiddleware)}");

            if (context.Request.Query.ContainsKey("terminate"))
            {
                await TerminatePipeline(context);
            }
            else
            {
                ClaimsIdentity authorizedUserClaim = CreateClaimsIdentity();
                AddIdentityToUserInContext(context, authorizedUserClaim);
                await _next(context);
            }

        }


        private async Task TerminatePipeline(HttpContext context)
        {
            HttpRequest request = context.Request;
            if (request.Query.ContainsKey("terminate"))
            {
                _logger.LogInformation("Terminates inside {Class}, because termination parameter was specified", nameof(AddPrincipalMiddleware));
                await context.Response.WriteAsync("Rotue is hijacked because of terminate query");
            }
        }

        private ClaimsIdentity CreateClaimsIdentity()
        {


            Claim[] claims = new Claim[2];
            Claim firstClaim = new Claim("customType", "NadarAlpenidzeAuthenticated");
            claims[0] = firstClaim;

            // User Principal
            UserPrincipal userPrincipal = new UserPrincipal { Name = "Nadar", Role = 1 };
            claims[1] = new Claim(ClaimTypes.UserData, JsonSerializer.Serialize(userPrincipal));

            ClaimsIdentity authorizedUserClaim = new ClaimsIdentity(claims, "CustomIdentity");
            return authorizedUserClaim;
        }


        private void AddIdentityToUserInContext(HttpContext context, ClaimsIdentity authorizedUserClaim)
        {
            context.User.AddIdentity(authorizedUserClaim);
            _logger.LogInformation("Added identity to request pipeline {@claimsIdentity}", authorizedUserClaim);
        }
    }
}
