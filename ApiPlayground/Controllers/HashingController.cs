using ApiPlayground.Infrastructure.Security.Hashing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Controllers
{
    [ApiController]
    [Route("api/hashing")]
    public class HashingController : ControllerBase
    {
        private readonly HashBuilder _hashBuilder;

        public HashingController(HashBuilder hashBuilder)
        {
            _hashBuilder = hashBuilder;
        }


        
        [HttpGet("{textString}")]
        public string GenerateHashFromString(string textString)
        {
            return _hashBuilder.Sha256(textString);
        }

        
        [HttpPost]
        public string GenerateHashFromPost(HmacShaModel shaModel)
        {
            return _hashBuilder.Sha256(shaModel.Message, shaModel.Key);
        }

    }
}
