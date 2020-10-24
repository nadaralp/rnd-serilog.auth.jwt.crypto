using ApiPlayground.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiPlayground.Controllers
{
    public class ControllerBaseExtended : ControllerBase
    {
        
        internal UserPrincipal GetUserPrincipal()
        {
            ClaimsIdentity customIdentity = Request
                .HttpContext
                .User
                .Identities
                .FirstOrDefault(i => i.AuthenticationType == "CustomIdentity");

            // ClaimsIdentity customIdentity = _identityFactory.GetIdentityByType(AuthenticationTypes.CustomIdentity);
            if (customIdentity is null)
                return null;

            Claim userDataClaim = customIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);
            return JsonSerializer.Deserialize<UserPrincipal>(userDataClaim.Value);

        }
    }
}
