using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcModule.Controllers
{

    [ApiController, Route("api/pets")]
    public class PetsController : ControllerBase
    {
        public string Test()
        {
            return "aviran";
        }
    }
}
