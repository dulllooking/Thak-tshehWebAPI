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

        /// <summary>
        /// 生成 JwtToken
        /// </summary>
        /// <param name="id">會員id</param>
        /// <returns>JwtToken</returns>
        public string GenerateToken(int id)
        {
            string secretKey = WebConfigurationManager.AppSettings["TokenKey"]; // 驗證用，要加密送出的 key
            var user = db.User.Find(id);
            string[] tempNickName = user.Account.Split('@');

            var payload = new Dictionary<string, object>
            {
                { "Id", user.Id },
                { "Account", user.Account },
                { "NickName", String.IsNullOrEmpty(user.NickName) ? tempNickName[0] : user.NickName },
                { "Image", String.IsNullOrEmpty(user.Image) ? "defaultProfile.jpg" : user.Image },
                { "Exp", DateTime.Now.AddMinutes(30).ToString() } // JwtToken 時效設定 30 分
            }; // payload 需透過 token 傳遞的資料

            var token = JWT.Encode(payload, Encoding.UTF8.GetBytes(secretKey), JwsAlgorithm.HS512); //產生 JwtToken
            return token;
        }

        /// <summary>
        /// 生成只刷新效期的 JwtToken
        /// </summary>
        /// <param name="userId">JWT解密後的Id</param>
        /// <param name="userAccount">JWT解密後的Account</param>
        /// <param name="userNickName">JWT解密後的NickName</param>
        /// <param name="userImage">JWT解密後的Image</param>
        /// <returns>JwtToken</returns>
        public string ExpRefreshToken(Dictionary<string, object> tokenData)
        {
            //單純刷新效期不新生成，新生成會進資料庫
            string secretKey = WebConfigurationManager.AppSettings["TokenKey"];
            var payload = new Dictionary<string, object>
            {
                { "Id", (int)tokenData["Id"] },
                { "Account", tokenData["Account"].ToString() },
                { "NickName", tokenData["NickName"].ToString() },
                { "Image", tokenData["Image"].ToString() },
                { "Exp", DateTime.Now.AddMinutes(30).ToString() } // JwtToken 時效設定 30 分
            }; //payload 需透過 token 傳遞的資料

            var token = JWT.Encode(payload, Encoding.UTF8.GetBytes(secretKey), JwsAlgorithm.HS512); //產生 JwtToken
            return token;
        }


        /// <summary>
        /// 生成無效 JwtToken
        /// </summary>
        /// <returns>JwtToken</returns>
        public string RevokeToken()
        {
            string secretKey = "RevokeToken";
            var payload = new Dictionary<string, object>
            {
                { "Id", 0 },
                { "Account", "None" },
                { "NickName", "None" },
                { "Image", "None" },
                { "Exp", DateTime.Now.AddDays(-15).ToString() } // 使 JwtToken 過期 失效
            }; // payload 需透過 JwtToken 傳遞的資料

            var token = JWT.Encode(payload, Encoding.UTF8.GetBytes(secretKey), JwsAlgorithm.HS512); //產生 JwtToken
            return token;
        }
    }
}