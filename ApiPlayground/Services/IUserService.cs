using ApiPlayground.Infrastructure;
using ApiPlayground.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiPlayground.Services
{
    public interface IUserService
    {
        Task<SecureUser> GetUserByNameAsync(string name);
        Task<ICollection<SecureUser>> GetUsersAsync();
        Task<Response<SecureUser>> CreateUserAsync(User user);
        Task<bool> IsUserCredentialsValidAsync(User user);
        ICollection<Claim> GenerateClaimsForUser(SecureUser secureUser);
    }
}
