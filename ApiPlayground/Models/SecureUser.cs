﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPlayground.Models
{
    public class SecureUser
    {
        public string Name { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; } // Will be saved hashed
    }
}
