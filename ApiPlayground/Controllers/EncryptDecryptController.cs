using ApiPlayground.Infrastructure.Security.Encryption;
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
    [Route("api/encryption")]
    public class EncryptDecryptController : ControllerBase
    {
        byte[] InitializationVector = Encoding.ASCII.GetBytes("83da0bfa15a640be9ca81276ce0d2874");
        byte[] Key = new byte[] { 0xAB, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        private readonly Encryptor _encryptor;

        public EncryptDecryptController(Encryptor encryptor)
        {
            _encryptor = encryptor;
        }


        [HttpGet("{plainText}")]
        public ActionResult<string> EncryptString(string plainText)
        {
            return _encryptor.EncryptStringToBytes(plainText, Key, InitializationVector).AsHexString();
        }


        [HttpGet("decrypt/{cipherText}")]
        public ActionResult<string> DecryptString(string cipherText)
        {
            byte[] bytes = cipherText.StringToByteArray();
            return _encryptor.DecryptStringFromBytes(bytes, Key, InitializationVector);
        }

    }
}
