using ApiPlayground.Infrastructure;
using ApiPlayground.Models;
using ApiPlayground.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersAuthController : ControllerBase
    {
        private readonly Services.IAuthenticationService _authenticationService;

        public UsersAuthController(Services.IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }



        [HttpPost]
        public async Task<ActionResult<Response<string>>> SignIn(User user)
        {
            string JWT = await _authenticationService.ChallengeUserAndReturnJwt(user);
            if (string.IsNullOrEmpty(JWT))
                return BadRequest("Unable to authenticated, check your credentials.");

            return Infrastructure.Response.OkResponse(JWT, "Jwt was created successfully.");

        }
    }
}
