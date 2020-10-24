using ApiPlayground.Infrastructure.Security.Policies.Requirements;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Infrastructure.Security.Policies
{
    public static class AddPoliciesStartup
    {
        public static void AddSecurtyPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyNames.AgeOver18,
                    policy => policy.Requirements.Add(new AgeRequirement(18)));
            });
        }
    }
}
