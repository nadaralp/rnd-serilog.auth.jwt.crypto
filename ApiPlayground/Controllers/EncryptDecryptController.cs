using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Controllers
{
    [ApiController]
    [Route("api/encryption")]
    public class EncryptDecryptController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> EncryptString()
        {
            return "";
        }


        [HttpGet("decrypt")]
        public ActionResult<string> DecryptString()
        {
            return "";
        }

    }
}
