﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class AuthModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class ValidateModel
    {
        public string Token { get; set; }
    }
}
