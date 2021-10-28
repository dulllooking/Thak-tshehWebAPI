using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Security
{
    public class LoginRequest
    {
        public string Account { get; set; }

        public string Password { get; set; }

        public string CheckPassword { get; set; }

        public string Guid { get; set; }

        public string Message { get; set; }

        public int ActivityId { get; set; }

        public int FollowingUserId { get; set; }

    }
}