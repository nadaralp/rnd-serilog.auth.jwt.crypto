using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ApiPlayground.Infrastructure.Security.Jwt
{
    public interface ITokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);
        string GenerateToken(string secret, string issuer, string audience, DateTime validityDate, IEnumerable<Claim> claims);
    }
}