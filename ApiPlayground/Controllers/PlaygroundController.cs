using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiPlayground.Infrastructure.Security.Policies;
using ApiPlayground.Models;
using ClassLibrary1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ApiPlayground.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaygroundController : ControllerBaseExtended
    {
        // ILogger is an interface which means we can plug in any type of concrete logger as long as it implements ILogger<T>.
        // We can change that to use serialog
        private readonly ILogger<PlaygroundController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Service1 _service1;
        private readonly IConfiguration _configuration;
        private int rndNumber = new Random().Next(1, 5);

        public PlaygroundController(
            ILogger<PlaygroundController> logger,
            IHttpContextAccessor httpContextAccessor,
            Service1 service1,
            IConfiguration configuration)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _service1 = service1;
            _configuration = configuration;
        }


        [HttpGet]
        public object Index()
        {

            if (rndNumber == 3)
            {
                try
                {
                    throw new Exception();
                }
                catch (Exception e)
                {
                    // It is a structured event logging. The position is what counts
                    _logger.LogError(e, "Caught exception, rndNumber={a}", rndNumber);
                }
            }

            UserPrincipal userPrincipal = GetUserPrincipal();
            _logger.LogDebug("user Principal = {@userPrincipal}", userPrincipal);

            return new
            {
                name = userPrincipal?.Name ?? "Name wasn't found"
            };
        }


        [HttpGet("age")]
        //[Authorize(Policy = PolicyNames.AgeOver18)]
        public dynamic AuthorizeAgeOver18()
        {
            ClaimsPrincipal user = User;
            return "Authorized successfully";
        }



        [HttpGet("service1")]
        public string DemoForAmit()
        =>
            _service1.CallService2PrintFromService1();

        [HttpGet("environments")]
        public string EnvironmentVariables()
        {
            // order of env variables:
            // appsettings.json -> {environment}.appsettings.json -> secrets.json -> environment variables
            return _configuration.GetSection("EnvDemo:Nested1:Name").Value ?? "Not found";
        }

    }
}
