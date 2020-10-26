using ApiPlayground.Infrastructure.Security.Hashing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Services
{
    public class PasswordService
    {
        private readonly HashBuilder _hashBuilder;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PasswordService> _logger;

        public PasswordService(HashBuilder hashBuilder, IConfiguration configuration, ILogger<PasswordService> logger)
        {
            _hashBuilder = hashBuilder;
            _configuration = configuration;
            _logger = logger;
        }


        public string GeneratePassword(string password, string salt)
        {
            string saltedPassowrd = password + salt;
            string key = _configuration.GetSection("Security:HmacSha256HashingKey").Value;
            if(string.IsNullOrEmpty(key))
            {
                _logger.LogWarning("HmacSha256 Security key={key} is empty. Was requested in ApiPlayground.Services.GeneratePassword", key);
            }
            return _hashBuilder.Sha256(saltedPassowrd, key);
        }
    }
}
