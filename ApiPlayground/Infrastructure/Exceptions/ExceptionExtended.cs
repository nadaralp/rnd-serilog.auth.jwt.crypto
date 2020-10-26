using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Infrastructure.Exceptions
{
    public class ExceptionExtended : Exception
    {
        public string GetMessageAndStackTraceInformation 
            => $"{Message} {StackTrace}";
        
    }
}
