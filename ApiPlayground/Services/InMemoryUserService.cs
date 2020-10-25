using ApiPlayground.Infrastructure;
using ApiPlayground.Infrastructure.Security.Hashing;
using ApiPlayground.InMemoryCache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        public InMemoryUserService(
            IMemoryCache memoryCache,
            SeedUsers seedUsers,
            SaltGenerator saltGenerator,
            HashBuilder hashBuilder,
            ILogger<InMemoryUserService> logger)
        {
            _memoryCache = memoryCache;
            _seedUsers = seedUsers;
            _saltGenerator = saltGenerator;
            _hashBuilder = hashBuilder;
            _logger = logger;
        }



        public Task<Response<SecureUser>> CreateUser(User user)
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
                Password = _hashBuilder.Sha256(user.Password, userSalt)
            };

            users.Add(newSecureUser);
            _memoryCache.Set(CacheKeys.UserData, users);

            return 
                Task.FromResult(
                    Response.OkResponse(newSecureUser, "User was added successfully")
                );
        }


        public Task<ICollection<SecureUser>> GetUsers()
        {
            ICollection<SecureUser> secureUsers = _memoryCache.GetOrCreate(CacheKeys.UserData, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);
                entry.SlidingExpiration = TimeSpan.FromSeconds(15);
                return _seedUsers.GetSecureUsers();
            });

            return Task.FromResult(secureUsers);
        }
    }
}
