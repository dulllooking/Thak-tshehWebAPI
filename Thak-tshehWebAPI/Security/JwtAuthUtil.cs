using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using Thak_tshehWebAPI.Models;

namespace Thak_tshehWebAPI.Security
{
    public class JwtAuthUtil
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public string GenerateToken(int id)
        {
            string Key = WebConfigurationManager.AppSettings["TokenKey"]; //加解密的 key,如果不一樣會無法成功解密
            var user = db.User.Find(id);

            var payload = new Dictionary<string, object>
            {
                { "id", user.Id },
                { "account", user.Account },
                { "name", user.Name?? "" },
                { "nickName", user.NickName?? "" },
                { "image", user.Image?? "" },
                { "exp", DateTime.Now.AddMinutes(15).ToString() } //Token 時效設定15分
            };//payload 需透過token傳遞的資料

            var token = JWT.Encode(payload, Encoding.UTF8.GetBytes(Key), JwsAlgorithm.HS512);//產生token
            return token;
        }
    }
}