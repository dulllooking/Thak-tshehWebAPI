﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Security
{
    public class LoginRequest
    {
        public string account { get; set; }

        public string password { get; set; }

        public string guid { get; set; }
    }
}