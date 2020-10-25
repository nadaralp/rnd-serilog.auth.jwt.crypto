
using ApiPlayground.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Infrastructure.Security.Jwt
{
    public class JwtTokenService
    {
        private readonly IOptions<JwtOptions> _options;
        private readonly ILogger<JwtTokenService> _logger;

        public JwtTokenService(IOptions<JwtOptions> options, ILogger<JwtTokenService> logger)
        {
            _options = options;
            _logger = logger;
        }


        /// <summary>
        /// Uses configuration from app settings.json, JWT section, to generate the token.
        /// </summary>
        /// <returns></returns>
        public string GenerateToken()
        {
            string validityInMinutes = _options.Value.ValidityInMinutes;


            if (!int.TryParse(validityInMinutes, out int minutes))
            {
                _logger.LogWarning("GenerateToken was called without minutes variable specified in JWT config. Can't continue operation and error was thrown");
                throw new InvalidOperationException("specify ValidityInMinutes in config file to call GenerateToken() parameterless");
            }


            return GenerateToken(
                _options.Value.Secret,
                _options.Value.Issuer,
                _options.Value.Audience,
                DateTime.UtcNow.AddMinutes(minutes)
                );
        }

        public string GenerateToken(string secret, string issuer, string audience, DateTime validityDate)
        {
            ICollection<Claim> claims = new List<Claim>();
            Claim claim1 = new Claim(ClaimTypes.Name, "nadar");
            Claim claim2 = new Claim(ClaimTypes.Email, "nadaralp16@gmail.com");

            claims.Add(claim1);
            claims.Add(claim2);

            byte[] secretBuffer = Encoding.ASCII.GetBytes(secret);
            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: validityDate,
                claims: claims,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretBuffer), SecurityAlgorithms.HmacSha256Signature)
                );


            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }
    }
}
