using Jose;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Thak_tshehWebAPI.Security
{
    // JwtAuthFilter 繼承 ActionFilterAttribute 可生成 [JwtAuthFilter] 使用
    public class JwtAuthFilter : ActionFilterAttribute
    {
        //加解密的 key,如果不一樣會無法成功解密
        private static readonly string secretKey = WebConfigurationManager.AppSettings["TokenKey"];

        public static Dictionary<string, object> GetToken(string token)
        {
            return JWT.Decode<Dictionary<string, object>>(token, Encoding.UTF8.GetBytes(secretKey), JwsAlgorithm.HS512);
        }

        //public static int GetTokenId(string token)
        //{
        //    var tokenData = JWT.Decode<Dictionary<string, object>>(token, Encoding.UTF8.GetBytes(secretKey), JwsAlgorithm.HS512);
        //    return (int)tokenData["Id"];
        //}

        //public static string GetTokenAccount(string token)
        //{
        //    var tokenData = JWT.Decode<Dictionary<string, object>>(token, Encoding.UTF8.GetBytes(secretKey), JwsAlgorithm.HS512);
        //    return tokenData["Account"].ToString();
        //}

        //public static string GetTokenNickName(string token)
        //{
        //    var tokenData = JWT.Decode<Dictionary<string, object>>(token, Encoding.UTF8.GetBytes(secretKey), JwsAlgorithm.HS512);
        //    return tokenData["NickName"].ToString();
        //}

        //public static string GetTokenImage(string token)
        //{
        //    var tokenData = JWT.Decode<Dictionary<string, object>>(token, Encoding.UTF8.GetBytes(secretKey), JwsAlgorithm.HS512);
        //    return tokenData["Image"].ToString();
        //}

        // 配合前端專案開發期限，將請求失敗暫時先改成 200 搭配 Status: false
        // 過濾有用標籤 [JwtAuthFilter] 請求的 API 的 JwtToken 狀態
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string secretKey = WebConfigurationManager.AppSettings["TokenKey"]; // 加解密的 key,如果不一樣會無法成功解密
            var request = actionContext.Request;
            // 有 JwtToken 時
            if (!WithoutVerifyToken(request.RequestUri.ToString())) {
                // 授權格式不存在且不正確時
                if (request.Headers.Authorization == null || request.Headers.Authorization.Scheme != "Bearer") {
                    // JwtToken 遺失，需導引重新登入
                    string messageJson = JsonConvert.SerializeObject(new { Status = false, Message = "請重新登入" });
                    var errorMessage = new HttpResponseMessage()
                    {
                        ReasonPhrase = "JwtToken Lost",
                        Content = new StringContent(messageJson,
                                    Encoding.UTF8,
                                    "application/json"),
                    };
                    throw new HttpResponseException(errorMessage);
                }
                else {
                    try {
                        // 有 JwtToken 且授權格式正確時
                        // 用 try 包住，因為如果有篡改可能解密失敗
                        // 解密後會回傳 Json 格式的物件 (即加密前的資料)
                        var jwtObject = JWT.Decode<Dictionary<string, Object>>(
                        request.Headers.Authorization.Parameter,
                        Encoding.UTF8.GetBytes(secretKey),
                        JwsAlgorithm.HS512);

                        // 檢查有效期限是否過期
                        // JwtToken 過期，需導引重新登入
                        if (IsTokenExpired(jwtObject["Exp"].ToString())) {
                            string messageJson = JsonConvert.SerializeObject(new { Status = false, Message = "請重新登入" });
                            var errorMessage = new HttpResponseMessage()
                            {
                                ReasonPhrase = "JwtToken Expired",
                                Content = new StringContent(messageJson,
                                    Encoding.UTF8,
                                    "application/json"),
                            };
                            throw new HttpResponseException(errorMessage);
                        }
                    }
                    catch (Exception) {
                        // 解密失敗
                        // JwtToken 不符，需導引重新登入
                        string messageJson = JsonConvert.SerializeObject(new { Status = false, Message = "請重新登入" });
                        var errorMessage = new HttpResponseMessage()
                        {
                            ReasonPhrase = "JwtToken NotMatch",
                            Content = new StringContent(messageJson,
                                    Encoding.UTF8,
                                    "application/json"),
                        };
                        throw new HttpResponseException(errorMessage);
                    }
                    
                }
            }

            base.OnActionExecuting(actionContext);
        }

        // 有在 Global 設定一律檢查 JwtToken 時才需設定排除
        // Login 不需要驗證因為還沒有 token
        public bool WithoutVerifyToken(string requestUri)
        {
            //if (requestUri.EndsWith("/login"))
            //    return true;
            return false;
        }

        // 驗證 token 時效
        public bool IsTokenExpired(string dateTime)
        {
            return Convert.ToDateTime(dateTime) < DateTime.Now;
        }

    }
}