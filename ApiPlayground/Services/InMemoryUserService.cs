using ApiPlayground.Infrastructure;
using ApiPlayground.Infrastructure.Exceptions.User;
using ApiPlayground.Infrastructure.Security.Hashing;
using ApiPlayground.InMemoryCache;
using ApiPlayground.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiPlayground.Services
{
    public class InMemoryUserService : IUserService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly SeedUsers _seedUsers;
        private readonly SaltGenerator _saltGenerator;
        private readonly HashBuilder _hashBuilder;
        private readonly ILogger<InMemoryUserService> _logger;
        private readonly PasswordService _passwordService;

        public InMemoryUserService(
            IServiceProvider serviceProvider,
            IMemoryCache memoryCache,
            SeedUsers seedUsers,
            SaltGenerator saltGenerator,
            HashBuilder hashBuilder,
            ILogger<InMemoryUserService> logger,
            PasswordService passwordService)
        {
            _memoryCache = memoryCache;
            _seedUsers = seedUsers;
            _saltGenerator = saltGenerator;
            _hashBuilder = hashBuilder;
            _logger = logger;
            _passwordService = passwordService;


            // Use this when the dependency is not frequently used
            _saltGenerator = serviceProvider.GetRequiredService<SaltGenerator>();

        }



        public Task<Response<SecureUser>> CreateUserAsync(User user)
        {
            _memoryCache.TryGetValue(CacheKeys.UserData, out ICollection<SecureUser> users);
            _logger.LogInformation("users data={@users}", users);
            if (users is null)
            {
                _logger.LogInformation("users are null {@users}", users);
                users = _seedUsers.GetSecureUsers();
            }


            string userSalt = _saltGenerator.GenerateSalt();
            //SecureUser newSecureUser = _userSecurityService.CreateSecureUser(user);
            SecureUser newSecureUser = new SecureUser
            {
                Name = user.Name,
                Salt = userSalt,
                Password = _passwordService.GeneratePassword(user.Name, userSalt)
            };

            users.Add(newSecureUser);
            _memoryCache.Set(CacheKeys.UserData, users);

            return
                Task.FromResult(
                    Response.OkResponse(newSecureUser, "User was added successfully")
                );
        }

        public async Task<SecureUser> GetUserByNameAsync(string name)
        {
            ICollection<SecureUser> secureUsers = await GetUsersAsync();
            return secureUsers.FirstOrDefault(u => string.Equals(name, u.Name));
        }


        public Task<ICollection<SecureUser>> GetUsersAsync()
        {
            ICollection<SecureUser> secureUsers = _memoryCache.GetOrCreate(CacheKeys.UserData, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                entry.SlidingExpiration = TimeSpan.FromSeconds(15);
                return _seedUsers.GetSecureUsers();
            });

            return Task.FromResult(secureUsers);
        }


        public async Task<bool> IsUserCredentialsValidAsync(User user)
        {
            // Query for user's salt
            var secureUserByName = await GetUserByNameAsync(user.Name); // we need the salt to compare passwords
            if (secureUserByName is null)
            {
                _logger.LogInformation("{@user} - user name doesn't exists in records. Was requested to match for salt on login.", user);
                throw new InvalidUserCredentialsException();
            }

            string password = _passwordService.GeneratePassword(user.Password, secureUserByName.Salt);
            if (string.Equals(password, secureUserByName.Password))
            {
                return true;
            }

            return false;
        }


        public ICollection<Claim> GenerateClaimsForUser(SecureUser secureUser)
        {
            List<Claim> claims = new List<Claim>();
            Claim nameClaim = new Claim(ClaimTypes.Name, secureUser.Name);

            // Adding the claims
            claims.Add(nameClaim);

            return claims;
        }
    }
}
