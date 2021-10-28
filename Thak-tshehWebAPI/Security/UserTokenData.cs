using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Security
{
    public class UserTokenData
    {
        // JWT payload 透過 token 傳遞的資料
        public string Id { get; set; }

        public string Account { get; set; }

        public string NickName { get; set; }

        public string Image { get; set; }

    }
}