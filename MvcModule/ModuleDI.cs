using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcModule
{
    public class ModuleDI : IStartupSetup
    {
        public void AddDependenciesForLayer(IServiceCollection services, IConfiguration configuration)
        {
            throw new NotImplementedException();
        }
    }
}
