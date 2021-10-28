using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Security
{
    public class UserTokenData
    {
        //JWT payload 透過token傳遞的資料
        public string id { get; set; }

        public string account { get; set; }

        public string name { get; set; }

        public string nickName { get; set; }

        public string image { get; set; }
    }
}