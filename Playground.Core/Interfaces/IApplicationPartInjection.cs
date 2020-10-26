using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Core.Interfaces
{
    /// <summary>
    /// Assign this interface to a class to inject the assembly of that class into MVC parts.
    /// </summary>
    /// <remarks>
    /// The class has to be abstract
    /// </remarks>
    public interface IApplicationPartInjection
    {
    }
}
