using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Security
{
    public class Payload
    {
        //使用者資訊
        public UserTokenData info { get; set; }

        //過期時間
        public int exp { get; set; }
    }
}