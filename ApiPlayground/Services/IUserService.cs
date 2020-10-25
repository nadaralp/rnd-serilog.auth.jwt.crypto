using ApiPlayground.Infrastructure;
using ApiPlayground.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiPlayground.Services
{
    public interface IUserService
    {
        Task<SecureUser> GetUserByName(string name);
        Task<ICollection<SecureUser>> GetUsers();
        Task<Response<SecureUser>> CreateUser(User user);
        Task<SecureUser> ValidateCredentialsAndGetUser(User user);
    }
}
