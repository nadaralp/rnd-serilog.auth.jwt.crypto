using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiPlayground.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaygroundController : ControllerBase
    {
        // ILogger is an interface which means we can plug in any type of concrete logger as long as it implements ILogger<T>.
        // We can change that to use serialog
        private readonly ILogger<PlaygroundController> _logger;
        private int rndNumber = new Random().Next(1, 5);

        public PlaygroundController(ILogger<PlaygroundController> logger)
        {
            _logger = logger;
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

            _logger.LogInformation("insde @ GET api/playground");
            return new
            {
                name = "Unknown"
            };
        }

    }
}
