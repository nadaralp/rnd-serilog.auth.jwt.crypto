using ApiPlayground.Infrastructure.Exceptions.User;
using ApiPlayground.Infrastructure.Security.Jwt;
using ApiPlayground.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(JwtTokenService jwtTokenService, IUserService userService, ILogger<AuthenticationService> logger)
        {
            _jwtTokenService = jwtTokenService;
            _userService = userService;
            _logger = logger;
        }


        public async Task<string> ChallengeUserAndReturnJwt(User user)
        {
            SecureUser secureUser = await _userService.ValidateCredentialsAndGetUser(user);
            if (secureUser is null)
            {
                _logger.LogInformation("user doesn't exist in the database - {@user}", user);
                throw new InvalidUserCredentialsException();
            }

            throw new NotImplementedException();
        }
    }
}
