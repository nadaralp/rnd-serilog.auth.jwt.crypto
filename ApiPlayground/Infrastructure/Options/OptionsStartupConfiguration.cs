using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Infrastructure.Options
{
    public static class OptionsStartupConfiguration
    {
        public static void AddAssemblyOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<JwtOptions>()
                .Bind(configuration.GetSection("JwtSettings"));
        }
    }
}
