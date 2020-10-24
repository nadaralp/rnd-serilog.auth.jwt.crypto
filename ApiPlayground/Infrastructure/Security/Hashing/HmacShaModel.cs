using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Infrastructure.Security.Hashing
{
    public class HmacShaModel
    {
        public string Message { get; set; }
        public string Key { get; set; }
    }
}
