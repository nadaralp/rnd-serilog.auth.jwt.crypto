using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Core.Interfaces
{
    public interface IStartupSetup
    {
        void AddDependenciesForLayer(IServiceCollection services, IConfiguration configuration);
    }
}
