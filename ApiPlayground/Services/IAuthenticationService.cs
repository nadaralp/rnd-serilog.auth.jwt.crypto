using ApiPlayground.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Services
{
    public interface IAuthenticationService
    {
        Task<string> ChallengeUserAndReturnJwt(User user);
    }
}
