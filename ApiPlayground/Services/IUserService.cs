using ApiPlayground.Infrastructure;
using ApiPlayground.InMemoryCache;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiPlayground.Services
{
    public interface IUserService
    {
        Task<ICollection<SecureUser>> GetUsers();
        Task<Response<SecureUser>> CreateUser(User user);
    }
}
