using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Infrastructure.Security.Policies.Requirements
{
    public class AgeRequirement : IAuthorizationRequirement
    {
        public int Age { get; }

        public AgeRequirement(int age)
        {
            Age = age;
        }
    }

    public class AgeRequirementHandler : AuthorizationHandler<AgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequirement requirement)
        {
            throw new NotImplementedException();
        }
    }
}
