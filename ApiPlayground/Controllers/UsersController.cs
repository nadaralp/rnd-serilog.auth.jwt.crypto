using ApiPlayground.Infrastructure;
using ApiPlayground.InMemoryCache;
using ApiPlayground.Models;
using ApiPlayground.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly SeedUsers _seedUsers;
        private readonly IUserService _userService;

        public UsersController(IMemoryCache memoryCache, SeedUsers seedUsers, IUserService userService)
        {
            _memoryCache = memoryCache;
            _seedUsers = seedUsers;
            _userService = userService;
        }



        [HttpGet]
        public async Task<ICollection<SecureUser>> GetSecureUser()
        {
            var secureUsers = await _userService.GetUsersAsync();
            return secureUsers;
        }


        [HttpPost]
        public async Task<Response<SecureUser>> CreateUser(User user)
        {
            return await _userService.CreateUserAsync(user);

        }
    }
}
