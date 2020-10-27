using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class StartupSetup : IStartupSetup
    {
        //public static void AddClassLibrary1DIConfiguration(this IServiceCollection services)
        //{
        //    services.AddScoped<Service1>();
        //    services.AddScoped<Service2>();
        //}

        public void AddDependenciesForLayer(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<Service1>();
            services.AddScoped<Service2>();
        }
    }
}
