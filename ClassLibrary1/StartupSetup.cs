using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public static class StartupSetup
    {
        public static void AddClassLibrary1DIConfiguration(this IServiceCollection services)
        {
            services.AddScoped<Service1>();
            services.AddScoped<Service2>();


        }
    }
}
