using ApiPlayground.Infrastructure.Exceptions.User;
using ApiPlayground.Infrastructure.Security.Jwt;
using ApiPlayground.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(ITokenService jwtTokenService, IUserService userService, ILogger<AuthenticationService> logger)
        {
            _tokenService = jwtTokenService;
            _userService = userService;
            _logger = logger;
        }


        public async Task<string> ChallengeUserAndReturnJwt(User user)
        {
            bool areCredentialsValid = await _userService.IsUserCredentialsValidAsync(user);
            if(!areCredentialsValid) throw new InvalidUserCredentialsException();
            SecureUser secureUser = await _userService.GetUserByNameAsync(user.Name);
            ICollection<Claim> claims = _userService.GenerateClaimsForUser(secureUser);

            return _tokenService.GenerateToken(claims);
        }
    }
}
