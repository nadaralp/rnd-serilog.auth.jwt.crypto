using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Models
{
    public class UserPrincipal
    {
        public string Name { get; set; }
        public int Role { get; set; }
    }
}
