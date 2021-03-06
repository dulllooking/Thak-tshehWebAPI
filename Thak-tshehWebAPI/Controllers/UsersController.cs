using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using Thak_tshehWebAPI.Models;
using Thak_tshehWebAPI.Security;
using NSwag.Annotations;
using Thak_tshehWebAPI.Models.Vms;
using Thak_tshehWebAPI.Models.Attributes;
using System.IO;
using System.Text;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using Thak_tshehWebAPI.Models.Params;

namespace Thak_tshehWebAPI.Controllers
{

    /// <summary>
    /// 使用者操作功能
    /// </summary>
    [OpenApiTag("Users", Description = "使用者操作功能")]
    //[EnableCors("*", "*", "*")] // 官方跨域設定加入 Owin Swagger UI 生成 Startup.cs 導致失效
    public class UsersController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private const int expiryTime = 1; // 信箱開通碼效期

        // POST: api/users/contact-us
        /// <summary>
        /// 1-5 聯絡我們功能 (JWT)
        /// </summary>
        /// <param name="contactUsVm">留言資料</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPost]
        [SwaggerResponse(typeof(ApiJwtResult))]
        [Route("api/users/contact-us")]
        public IHttpActionResult SendContactUsMail(ContactUsVm contactUsVm)
        {
            // 必填欄位資料檢查
            if (contactUsVm.Message == null) return Ok(new ApiMessageResult { Status = false, Message = "未填欄位" });

            // 取出請求內容，解密 JwtToken 取出資料
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            //單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);

            // 將留言寄信至客服信箱
            string serviceMail = WebConfigurationManager.AppSettings["ServiceMail"];
            string userMessage = contactUsVm.Message;
            Mail.SendMessageToUs("作伙來讀冊客服信箱", serviceMail, userToken["NickName"].ToString(), userToken["Account"].ToString(), userMessage);

            // 送出刷新 JwtToken
            return Ok(new ApiJwtResult { Status = true, JwtToken = jwtToken });
        }

        // GET: api/users/profile-data
        /// <summary>
        /// 1-8 Navbar 個人資料+頭貼 (JWT)
        /// </summary>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpGet]
        [SwaggerResponse(typeof(ProfileJwtVm))]
        [Route("api/users/profile-data")]
        public IHttpActionResult GetProfileData()
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            //單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);

            // 回傳 JwtToken 及頭貼個資
            return Ok(new ProfileJwtVm
            {
                Status = true,
                JwtToken = jwtToken,
                Data = new ProfileData
                {
                    Id = (int)userToken["Id"],
                    Account = userToken["Account"].ToString(),
                    NickName = userToken["NickName"].ToString(),
                    Image = userToken["Image"].ToString()
                }
            });
        }

        // DELETE: api/users/logout
        /// <summary>
        /// 1-9 登出驗證票
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [SwaggerResponse(typeof(ApiJwtResult))]
        [Route("api/users/logout")]
        public IHttpActionResult Logout()
        {
            // 刷新 JwtToken 使其失效
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.RevokeToken();

            // 登出刷新
            return Ok(new ApiJwtResult { Status = true, JwtToken = jwtToken });
        }

        // GET: api/activities/new9/type/類型(0~2)
        /// <summary>
        /// 1-10 1-11 1-12 滾動區資料 (JWT)
        /// </summary>
        /// <param name="type">類型(0~2)</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpGet]
        [SwaggerResponse(typeof(ActivityViewsJwtVm))]
        [Route("api/users/activities/new9/type/{type}")]
        public IHttpActionResult GetNew9Activity(int type)
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            //單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            var info = db.Activity.AsNoTracking().Where(x => ((int)x.ActivityType) == type).OrderByDescending(x => x.CreatDate).Select(x => new ActivityViewsDataJWT
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                ActivityStartDate = x.ActivityStartDate,
                ActivityEndDate = x.ActivityEndDate,
                LimitNumber = x.LimitNumber,
                Summary = x.Summary,
                OrganizerName = x.OrganizerName,
                ActivityType = x.ActivityType,
                Views = x.Views,
                CreatDate = x.CreatDate,
                // 查關聯的表匿名函式命名要用不同
                UserCollected = x.ActivityCollect.Any(y => y.UserId == userId)
            }).Take(9).ToArray();

            return Ok(new ActivityViewsJwtVm { Status = true, JwtToken = jwtToken, Data = info });
        }

        // POST: api/users/sign-up
        /// <summary>
        /// 2-1 未開通註冊+發信
        /// </summary>
        /// <param name="userData">登入資料</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(typeof(ApiMessageResult))]
        [Route("api/users/sign-up")]
        public IHttpActionResult SignUp(LoginVm userData)
        {
            // 必填欄位資料檢查
            if (!ModelState.IsValid) return Ok(new ApiMessageResult { Status = false, Message = "欄位資料不符" });
            if (userData == null) return Ok(new ApiMessageResult { Status = false, Message = "未填欄位" });
            // 帳號已註冊過
            if (!(db.User.FirstOrDefault(x => x.Account == userData.Account) == null)) return Ok(new ApiMessageResult { Status = false, Message = "帳號已存在" });
            // 帳號信箱格式不符
            if (String.IsNullOrEmpty(userData.Account)) return Ok(new ApiMessageResult { Status = false, Message = "信箱未填" });
            else if (!Mail.RegexEmail(userData.Account)) return Ok(new ApiMessageResult { Status = false, Message = "信箱格式不符" });
            // 密碼格式不符
            if (String.IsNullOrEmpty(userData.Password)) return Ok(new ApiMessageResult { Status = false, Message = "密碼未填" });
            else if (!Mail.RegexPassword(userData.Password)) return Ok(new ApiMessageResult { Status = false, Message = "密碼格式不符" });

            try {
                // 生成密碼雜湊鹽
                string saltStr = HashPassword.CreateSalt();
                // 生成郵件連結驗證碼
                string mailGuid = Guid.NewGuid().ToString();
                // 生成使用者資料
                string[] accountArr = userData.Account.Split('@');

                User userInput = new User
                {
                    Account = userData.Account,
                    HashPassword = HashPassword.GenerateHashWithSalt(userData.Password, saltStr),
                    Salt = saltStr,
                    Image = "defaultprofile.jpg",
                    NickName = accountArr[0],
                    Sex = Sex.不公開,
                    Country = "讀冊國",
                    City = "學富市",
                    Area = "五車區",
                    AboutMe = "目前尚未有關於您的簡介，請輸入您的簡介。",
                    MySkill = "目前尚未有關於您的專長，請輸入您的專長。",
                    MyInterest = "目前尚未有關於您的興趣，請輸入您的興趣。",
                    ShowComingActivity = true,
                    ShowNeedEvalActivity = true,
                    ShowCollectActivity = true,
                    ShowFinishActivity = true,
                    ShowCancelActivity = true,
                    ShowFollowing = true,
                    ShowFollowers = true,
                    Views = 0,
                    AccountState = false,
                    CreatDate = DateTime.Now,
                    CheckMailCode = mailGuid,
                    MailCodeCreatDate = DateTime.Now.AddDays(expiryTime) // 設定驗證碼效期1天
                };
                // 加入資料並儲存
                db.User.Add(userInput);
                db.SaveChanges();

                string verifyLink = Mail.SetAuthMailLink(Request.RequestUri.Host, mailGuid);
                Mail.SendVerifyLinkMail(accountArr[0], userData.Account, verifyLink);
            }
            catch (Exception ex) {
                return Ok(new ApiMessageResult { Status = false, Message = ex.Message });
            }

            return Ok(new ApiMessageResult { Status = true, Message = "註冊成功，請至信箱點選開通帳號連結登入!" });
        }

        // POST: api/users/login
        /// <summary>
        /// 2-2 會員登入
        /// </summary>
        /// <param name="userData">登入資料</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(typeof(ApiJwtResult))]
        [Route("api/users/login")]
        public IHttpActionResult Login(LoginVm userData)
        {
            // 查詢指定帳號
            var userQuery = db.User.FirstOrDefault(x => x.Account == userData.Account);

            // 必填欄位資料檢查
            if (!ModelState.IsValid) return Ok(new ApiMessageResult { Status = false, Message = "未填欄位" });
            if (userData == null) return Ok(new ApiMessageResult { Status = false, Message = "未填欄位" });
            // 帳號信箱格式不符
            if (String.IsNullOrEmpty(userData.Account)) return Ok(new ApiMessageResult { Status = false, Message = "信箱未填" });
            else if (!Mail.RegexEmail(userData.Account)) return Ok(new ApiMessageResult { Status = false, Message = "信箱格式不符" });
            // 帳號檢查
            else if (userQuery == null) return Ok(new ApiMessageResult { Status = false, Message = "帳號不存在" });
            // 密碼格式不符
            if (String.IsNullOrEmpty(userData.Password)) return Ok(new ApiMessageResult { Status = false, Message = "密碼未填" });
            else if (!Mail.RegexPassword(userData.Password)) return Ok(new ApiMessageResult { Status = false, Message = "密碼格式不符" });

            // 登入密碼加鹽雜湊結果
            string hashPassword = HashPassword.GenerateHashWithSalt(userData.Password, userQuery.Salt);
            // 密碼檢查
            if (!(userQuery.HashPassword.Equals(hashPassword))) return Ok(new ApiMessageResult { Status = false, Message = "密碼不正確" });

            // 有網址傳值 guid 且驗證碼期限未到期就開通帳號
            string verifyLink;
            string[] accountArr;
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.GenerateToken(userQuery.Id);
            if (!userQuery.AccountState && userQuery.MailCodeCreatDate > DateTime.Now) {
                // 權限未開通且信箱開通連結未過期
                return Ok(new ApiMessageResult { Status = false, Message = "未開通驗證，請至信箱收信開通驗證" });
            }
            else if (!userQuery.AccountState) {
                // 權限未開通且信箱開通連結已過期，生成郵件連結驗證碼重寄驗證信
                string mailGuid = Guid.NewGuid().ToString();
                userQuery.CheckMailCode = mailGuid;
                userQuery.MailCodeCreatDate = DateTime.Now.AddDays(1);
                db.SaveChanges();

                verifyLink = Mail.SetAuthMailLink(Request.RequestUri.Host, mailGuid);
                accountArr = userData.Account.Split('@');
                Mail.SendVerifyLinkMail(accountArr[0], userData.Account, verifyLink);
                return Ok(new ApiMessageResult { Status = false, Message = "開通驗證連結過期，已重新發信，請至信箱收信重新驗證" });
            }

            // 一般登入
            return Ok(new ApiJwtResult { Status = true, JwtToken = jwtToken });
        }

        // Put: api/users/auth-mail
        /// <summary>
        /// 2-3 註冊開通
        /// </summary>
        /// <param name="mailData">信箱驗證資料</param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerResponse(typeof(ApiMessageResult))]
        [Route("api/users/auth-mail")]
        public IHttpActionResult AuthMail(VerifyMailVm mailData)
        {
            // 查詢指定帳號
            var userQuery = db.User.FirstOrDefault(x => x.CheckMailCode == mailData.Guid);

            if (mailData.Guid == null) return Ok(new ApiMessageResult { Status = false, Message = "未填欄位" });
            // 帳號檢查
            else if (userQuery == null) return Ok(new ApiMessageResult { Status = false, Message = "帳號驗證碼不存在" });

            // 判斷 guid 時效及開通狀態，驗證碼期限未到期就開通帳號
            string verifyLink;
            string[] accountArr;
            if (userQuery.AccountState) {
                // 帳號已開通
                return Ok(new ApiMessageResult { Status = false, Message = "帳號已開通，可直接登入使用" });
            }
            else if (userQuery.MailCodeCreatDate < DateTime.Now && !userQuery.AccountState) {
                // 驗證連結 guid 過期，生成郵件連結驗證碼重寄驗證信
                string mailGuid = Guid.NewGuid().ToString();
                userQuery.CheckMailCode = mailGuid;
                userQuery.MailCodeCreatDate = DateTime.Now.AddDays(1);
                verifyLink = Mail.SetAuthMailLink(Request.RequestUri.Host, mailGuid);
                accountArr = userQuery.Account.Split('@');
                Mail.SendVerifyLinkMail(accountArr[0], userQuery.Account, verifyLink);
                return Ok(new ApiMessageResult { Status = false, Message = "開通驗證連結過期，已重新發信，請至信箱收信重新驗證" });
            }

            // 開通更新
            userQuery.AccountState = true;
            db.SaveChanges();

            return Ok(new ApiMessageResult { Status = true, Message = "帳號開通成功，請重新登入!" });
        }

        // Put: api/users/forget-password-mail
        /// <summary>
        /// 2-4 忘記密碼發信
        /// </summary>
        /// <param name="accountData">帳號資料</param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerResponse(typeof(ApiMessageResult))]
        [Route("api/users/forget-password-mail")]
        public IHttpActionResult SendResetPasswordMail(AccountVm accountData)
        {
            // 查詢指定帳號
            var userQuery = db.User.FirstOrDefault(x => x.Account == accountData.Account);

            if (accountData.Account == null) return Ok(new ApiMessageResult { Status = false, Message = "未填欄位" });
            // 帳號檢查
            else if (userQuery == null) return Ok(new ApiMessageResult { Status = false, Message = "帳號不存在" });
            else if (!Mail.RegexEmail(accountData.Account)) return Ok(new ApiMessageResult { Status = false, Message = "信箱格式不符" });

            // 生成重設密碼連結，發信
            string resetLink;
            string[] accountArr;
            string mailGuid = Guid.NewGuid().ToString();
            userQuery.CheckMailCode = mailGuid;
            userQuery.MailCodeCreatDate = DateTime.Now;
            resetLink = Mail.SetResetPasswordMailLink(Request.RequestUri.Host, mailGuid);
            accountArr = userQuery.Account.Split('@');
            Mail.SendResetLinkMail(accountArr[0], userQuery.Account, resetLink);
            db.SaveChanges();

            return Ok(new ApiMessageResult { Status = true, Message = "已發送重設密碼連結至信箱，請至信箱收信點選連結重設密碼" });
        }

        // POST: api/users/auth-password
        /// <summary>
        /// 2-5 驗證舊密碼 (JWT)
        /// </summary>
        /// <param name="passwordData">密碼資料</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPost]
        [SwaggerResponse(typeof(ApiJwtResult))]
        [Route("api/users/auth-password")]
        public IHttpActionResult AuthUserPassword(PasswordVm passwordData)
        {
            // 必填欄位資料檢查 (需在類別加限制條件標籤才有效)
            if (!ModelState.IsValid) return Ok(new ApiMessageResult { Status = false, Message = "未填欄位" });
            if (passwordData == null) return Ok(new ApiMessageResult { Status = false, Message = "未填欄位" });
            // 密碼格式不符
            if (String.IsNullOrEmpty(passwordData.Password)) return Ok(new ApiMessageResult { Status = false, Message = "密碼未填" });
            else if (!Mail.RegexPassword(passwordData.Password)) return Ok(new ApiMessageResult { Status = false, Message = "密碼格式不符" });

            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            //單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);

            // 查詢指定帳號
            var userQuery = db.User.AsNoTracking().FirstOrDefault(x => x.Id == (int)userToken["Id"]);
            // 登入密碼加鹽雜湊結果
            string hashPassword = HashPassword.GenerateHashWithSalt(passwordData.Password, userQuery.Salt);
            // 密碼檢查
            if (!(userQuery.HashPassword.Equals(hashPassword))) return Ok(new ApiMessageResult { Status = false, Message = "密碼不正確" });

            // 送出刷新 JwtToken
            return Ok(new ApiJwtResult { Status = true, JwtToken = jwtToken });
        }

        // POST: api/users/login-reset-password
        /// <summary>
        /// 2-6 登入時重設密碼 (JWT)
        /// </summary>
        /// <param name="resetData">重設密碼資料</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPost]
        [SwaggerResponse(typeof(ApiJwtResult))]
        [Route("api/users/login-reset-password")]
        public IHttpActionResult LoginResetPassword(PasswordResetVm resetData)
        {
            // 必填欄位資料檢查
            if (!ModelState.IsValid) return Ok(new ApiMessageResult { Status = false, Message = "未填欄位" });
            if (resetData == null) return Ok(new ApiMessageResult { Status = false, Message = "未填欄位" });
            // 密碼格式不符
            if (String.IsNullOrEmpty(resetData.Password) || String.IsNullOrEmpty(resetData.CheckPassword)) return Ok(new ApiMessageResult { Status = false, Message = "密碼未填" });
            else if (!Mail.RegexPassword(resetData.Password) || !Mail.RegexPassword(resetData.CheckPassword)) return Ok(new ApiMessageResult { Status = false, Message = "密碼格式不符" });
            else if (!resetData.Password.Equals(resetData.CheckPassword)) return Ok(new ApiMessageResult { Status = false, Message = "填入密碼不同，請重新輸入" });

            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            //單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);

            try {
                // 查詢指定帳號
                var userQuery = db.User.FirstOrDefault(x => x.Id == (int)userToken["Id"]);
                // 生成密碼雜湊鹽
                string saltStr = HashPassword.CreateSalt();
                // 登入密碼加鹽雜湊結果
                string hashPassword = HashPassword.GenerateHashWithSalt(resetData.Password, saltStr);

                // 存入新密碼
                userQuery.Salt = saltStr;
                userQuery.HashPassword = hashPassword;
                db.SaveChanges();
            }
            catch (Exception ex) {

                return Ok(new ApiMessageResult { Status = false, Message = ex.Message });
            }

            // 刷新 JwtToken
            return Ok(new ApiJwtResult { Status = true, JwtToken = jwtToken });
        }

        // Post: api/users/mail-reset-password
        /// <summary>
        /// 2-7 信箱連入重設密碼
        /// </summary>
        /// <param name="resetData">信箱連入重設密碼資料</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse(typeof(ApiMessageResult))]
        [Route("api/users/mail-reset-password")]
        public IHttpActionResult MailLinkResetPassword(PasswordMailResetVm resetData)
        {
            // 查詢指定帳號
            var userQuery = db.User.FirstOrDefault(x => x.CheckMailCode == resetData.Guid);

            if (resetData.Guid == null) return Ok(new ApiMessageResult { Status = false, Message = "未填欄位" });
            else if (userQuery.MailCodeCreatDate.AddMinutes(30) > DateTime.Now ) return Ok(new ApiMessageResult { Status = false, Message = "連結驗證碼超過30分鐘" });
            // 帳號檢查
            else if (userQuery == null) return Ok(new ApiMessageResult { Status = false, Message = "連結驗證碼不存在" });
            // 密碼格式不符
            if (String.IsNullOrEmpty(resetData.Password) || String.IsNullOrEmpty(resetData.CheckPassword)) return Ok(new ApiMessageResult { Status = false, Message = "密碼未填" });
            else if (!Mail.RegexPassword(resetData.Password) || !Mail.RegexPassword(resetData.CheckPassword)) return Ok(new ApiMessageResult { Status = false, Message = "密碼格式不符" });
            else if (!resetData.Password.Equals(resetData.CheckPassword)) return Ok(new ApiMessageResult { Status = false, Message = "填入密碼不同，請重新輸入" });

            // 生成密碼雜湊鹽
            string saltStr = HashPassword.CreateSalt();
            // 登入密碼加鹽雜湊結果
            string hashPassword = HashPassword.GenerateHashWithSalt(resetData.Password, saltStr);

            // 更新密碼，註銷驗證碼
            userQuery.CheckMailCode = "";
            userQuery.Salt = saltStr;
            userQuery.HashPassword = hashPassword;
            db.SaveChanges();

            // 重設成功
            return Ok(new ApiMessageResult { Status = true, Message = "密碼重設成功，請重新登入" });
        }

        // GET: api/users/activity/search/每頁幾筆/第幾頁/類型/分類/地區/排序/關鍵字 (用空格區隔最多可查兩個關鍵字)
        /// <summary>
        /// 3-1 已登入分類搜尋+排序+分頁功能 (JWT)
        /// </summary>
        /// <param name="searchActivityParam">活動搜尋參數</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpGet]
        [SwaggerResponse(typeof(SearchActivityJwtVm))]
        [Route("api/users/activity/search/{split:int?}/{page:int?}/{type:int?}/{classify:int?}/{area:int?}/{sorting:int?}/{query?}")]
        public IHttpActionResult GetSearchActivity([FromUri] SearchActivityParam searchActivityParam)
        {
            SearchActivityParam param = new SearchActivityParam();
            param = searchActivityParam;
            // 前端會使用encodeURI(關鍵字)，因此需要解碼
            string decodeKeyWord = HttpUtility.UrlDecode(param.Query).Trim();
            // 切割取出兩組關鍵字
            string[] keyWordArr = decodeKeyWord.Split(' ');
            string keyWord1 = keyWordArr[0].ToString();
            string keyWord2 = keyWordArr.Length > 1 ? keyWordArr[1].ToString() : "~!@#$%^&*()_+";

            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            // var info = db.Activity; // 沒加查詢語句時型別是 DBset 後面無法再取出查詢
            // 多個獨立條件需分開用.Where()，不然結果不是預期的東西
            var info = db.Activity.AsNoTracking().Where(x => param.Type.Value < 0 || (int)x.ActivityType == param.Type.Value)
                .Where(x => param.Classify.Value < 0 || (int)x.ActivityClass == param.Classify.Value)
                .Where(x => (x.Name.Contains(keyWord1) || x.OrganizerName.Contains(keyWord1) || x.Summary.Contains(keyWord1) || x.ContentText.Contains(keyWord1))
               || (x.Name.Contains(keyWord2) || x.OrganizerName.Contains(keyWord2) || x.Summary.Contains(keyWord2) || x.ContentText.Contains(keyWord2)))
                .Where(x => param.Area.Value == 0 ? (int)x.Area <= 6 : param.Area.Value == 1 ? ((int)x.Area > 6 || (int)x.Area <= 11) : param.Area.Value == 2 ? ((int)x.Area > 11 || (int)x.Area <= 16) : param.Area.Value == 3 ? ((int)x.Area > 16 || (int)x.Area <= 18) : param.Area.Value == 4 ? (int)x.Area > 18 : (int)x.Area >= 0)
                .Select(x => new ActivityDataJWT
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    ActivityStartDate = x.ActivityStartDate,
                    ActivityEndDate = x.ActivityEndDate,
                    LimitNumber = x.LimitNumber,
                    Summary = x.Summary,
                    OrganizerName = x.OrganizerName,
                    ActivityType = x.ActivityType,
                    ActivityClass = x.ActivityClass,
                    ActivityArea = x.Area,
                    Views = x.Views,
                    ApplicantNumber = x.ApplicantNumber,
                    CollectNumber = x.CollectNumber,
                    EvaluateStars = x.EvaluateStars,
                    OpinionNumber = x.OpinionNumber,
                    Price = x.Price,
                    StartAcceptDate = x.StartAcceptDate,
                    EndAcceptDate = x.EndAcceptDate,
                    // 查關聯的表匿名函式命名要用不同
                    UserCollected = x.ActivityCollect.Any(y => y.UserId == userId)
                });

            switch (param.Sorting.Value) {
                case 0: // 最新發佈 (預設) => 以活動開始時間排序，由近到遠
                    info = info.OrderByDescending(x => x.StartAcceptDate);
                    break;
                case 1: // 最熱門活動 => 以瀏覽人數排序，由多到少
                    info = info.OrderByDescending(x => x.Views);
                    break;
                case 2: // 日期由新到舊 => 以開放報名時間排序，由近到遠
                    info = info.OrderBy(x => x.ActivityStartDate);
                    break;
                default:
                    break;
            }

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)info.Count() / param.Split.Value);

            // .Skip() 前一定要接 .OrderBy()
            // 刷新 JwtToken 並送出活動資料
            return Ok(new SearchActivityJwtVm
            {
                Status = true,
                JwtToken = jwtToken,
                Data = new SearchActivityDataJWT
                {
                    TotalPage = totalPageNumber,
                    Activity = info.Skip(param.Split.Value * (param.Page.Value - 1)).Take(param.Split.Value).ToArray()
                }
            });
        }

        // Put: api/users/activity/collect
        /// <summary>
        /// 3-2 收藏/取消收藏活動 (JWT)
        /// </summary>
        /// <param name="activityData">活動基本資料</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPut]
        [SwaggerResponse(typeof(ApiMessageJwtResult))]
        [Route("api/users/activity/collect")]
        public IHttpActionResult SetCollect(ActivityDataVm activityData)
        {
            if (activityData.ActivityId <= 0) return Ok(new ApiMessageResult { Status = false, Message = "此活動不存在" });

            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            // 查詢指定活動收藏記錄
            var collectQuery = db.ActivityCollect.Where(x => x.UserId == userId);
            string messageStr = "新增收藏成功";
            try {
                // 活動收藏執行判斷
                if (collectQuery.Any(x => x.ActivityId == activityData.ActivityId)) {
                    // 已有收藏記錄時刪除記錄
                    db.ActivityCollect.Remove(collectQuery.FirstOrDefault(x => x.ActivityId == activityData.ActivityId));
                    db.SaveChanges();
                    messageStr = "取消收藏成功";
                    return Ok(new ApiMessageJwtResult { Status = true, JwtToken = jwtToken, Message = messageStr });
                }

                // 沒有收藏記錄時增加記錄
                ActivityCollect activityCollect = new ActivityCollect
                {
                    UserId = userId,
                    ActivityId = activityData.ActivityId,
                    CreatDate = DateTime.Now
                };
                db.ActivityCollect.Add(activityCollect);
                db.Activity.FirstOrDefault(x => x.Id == activityData.ActivityId).CollectNumber += 1;
                db.SaveChanges();
            }
            catch (Exception ex) {

                return Ok(new ApiMessageResult { Status = false, Message = ex.Message });
            }

            return Ok(new ApiMessageJwtResult { Status = true, JwtToken = jwtToken, Message = messageStr });
        }

        // GET: api/users/activity/final/type/類型/每頁幾筆/第幾頁
        /// <summary>
        /// 4-5 即將截止報名資料 (JWT)
        /// </summary>
        /// <param name="recommendActivityParam">官方推薦參數</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpGet]
        [SwaggerResponse(typeof(SearchActivityJwtVm))]
        [Route("api/users/activity/final/type/{type:int?}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetFinal4HourActivity([FromUri] RecommendActivityParam recommendActivityParam)
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            RecommendActivityParam param = new RecommendActivityParam();
            param = recommendActivityParam;
            var info = db.Activity.AsNoTracking().Where(x => (int)x.ActivityType == param.Type.Value && x.EndAcceptDate > DateTime.Now && !x.ApplicantFull).Select(x => new ActivityDataJWT
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                ActivityStartDate = x.ActivityStartDate,
                ActivityEndDate = x.ActivityEndDate,
                LimitNumber = x.LimitNumber,
                Summary = x.Summary,
                OrganizerName = x.OrganizerName,
                ActivityType = x.ActivityType,
                ActivityClass = x.ActivityClass,
                ActivityArea = x.Area,
                Views = x.Views,
                ApplicantNumber = x.ApplicantNumber,
                CollectNumber = x.CollectNumber,
                EvaluateStars = x.EvaluateStars,
                OpinionNumber = x.OpinionNumber,
                Price = x.Price,
                StartAcceptDate = x.StartAcceptDate,
                EndAcceptDate = x.EndAcceptDate,
                // 查關聯的表匿名函式命名要用不同
                UserCollected = x.ActivityCollect.Any(y => y.UserId == userId)
            }).OrderBy(x => x.EndAcceptDate);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)info.Count() / param.Split.Value);

            // 刷新 JwtToken 並送出活動資料
            return Ok(new SearchActivityJwtVm
            {
                Status = true,
                JwtToken = jwtToken,
                Data = new SearchActivityDataJWT
                {
                    TotalPage = totalPageNumber,
                    Activity = info.Skip(param.Split.Value * (param.Page.Value - 1)).Take(param.Split.Value).ToArray()
                }
            });
        }

        // GET: api/users/activity/hot/type/類型/每頁幾筆/第幾頁
        /// <summary>
        /// 4-6 最多人報名資料 (JWT)
        /// </summary>
        /// <param name="recommendActivityParam">官方推薦參數</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(SearchActivityJwtVm))]
        [Route("api/users/activity/hot/type/{type:int?}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetHotActivity([FromUri] RecommendActivityParam recommendActivityParam)
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            RecommendActivityParam param = new RecommendActivityParam();
            param = recommendActivityParam;
            var info = db.Activity.AsNoTracking().Where(x => (int)x.ActivityType == param.Type.Value && x.EndAcceptDate > DateTime.Now && !x.ApplicantFull).Select(x => new ActivityDataJWT
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                ActivityStartDate = x.ActivityStartDate,
                ActivityEndDate = x.ActivityEndDate,
                LimitNumber = x.LimitNumber,
                Summary = x.Summary,
                OrganizerName = x.OrganizerName,
                ActivityType = x.ActivityType,
                ActivityClass = x.ActivityClass,
                ActivityArea = x.Area,
                Views = x.Views,
                ApplicantNumber = x.ApplicantNumber,
                CollectNumber = x.CollectNumber,
                EvaluateStars = x.EvaluateStars,
                OpinionNumber = x.OpinionNumber,
                Price = x.Price,
                StartAcceptDate = x.StartAcceptDate,
                EndAcceptDate = x.EndAcceptDate,
                // 查關聯的表匿名函式命名要用不同
                UserCollected = x.ActivityCollect.Any(y => y.UserId == userId)
            }).OrderByDescending(x => x.ApplicantNumber);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)info.Count() / param.Split.Value);

            // 刷新 JwtToken 並送出活動資料
            return Ok(new SearchActivityJwtVm
            {
                Status = true,
                JwtToken = jwtToken,
                Data = new SearchActivityDataJWT
                {
                    TotalPage = totalPageNumber,
                    Activity = info.Skip(param.Split.Value * (param.Page.Value - 1)).Take(param.Split.Value).ToArray()
                }
            });
        }

        // GET: api/users/activity/new/type/類型/每頁幾筆/第幾頁
        /// <summary>
        /// 4-7 新推出資料 (JWT)
        /// </summary>
        /// <param name="recommendActivityParam">官方推薦參數</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(SearchActivityJwtVm))]
        [Route("api/users/activity/new/type/{type:int?}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetNewActivity([FromUri] RecommendActivityParam recommendActivityParam)
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            RecommendActivityParam param = new RecommendActivityParam();
            param = recommendActivityParam;
            var info = db.Activity.AsNoTracking().Where(x => (int)x.ActivityType == param.Type.Value && x.EndAcceptDate > DateTime.Now && !x.ApplicantFull).Select(x => new ActivityDataJWT
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                ActivityStartDate = x.ActivityStartDate,
                ActivityEndDate = x.ActivityEndDate,
                LimitNumber = x.LimitNumber,
                Summary = x.Summary,
                OrganizerName = x.OrganizerName,
                ActivityType = x.ActivityType,
                ActivityClass = x.ActivityClass,
                ActivityArea = x.Area,
                Views = x.Views,
                ApplicantNumber = x.ApplicantNumber,
                CollectNumber = x.CollectNumber,
                EvaluateStars = x.EvaluateStars,
                OpinionNumber = x.OpinionNumber,
                Price = x.Price,
                StartAcceptDate = x.StartAcceptDate,
                EndAcceptDate = x.EndAcceptDate,
                // 查關聯的表匿名函式命名要用不同
                UserCollected = x.ActivityCollect.Any(y => y.UserId == userId)
            }).OrderByDescending(x => x.StartAcceptDate);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)info.Count() / param.Split.Value);

            // 刷新 JwtToken 並送出活動資料
            return Ok(new SearchActivityJwtVm
            {
                Status = true,
                JwtToken = jwtToken,
                Data = new SearchActivityDataJWT
                {
                    TotalPage = totalPageNumber,
                    Activity = info.Skip(param.Split.Value * (param.Page.Value - 1)).Take(param.Split.Value).ToArray()
                }
            });
        }

        // GET: api/users/activity/attend/status/活動ID
        /// <summary>
        /// 5-4 確認是否已報名過活動 (JWT)
        /// </summary>
        /// <param name="id">活動ID</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpGet]
        [SwaggerResponse(typeof(ApiMessageJwtResult))]
        [Route("api/users/activity/attend/status/{id}")]
        public IHttpActionResult GetAttendStatus(int id)
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];
            // 查詢報名紀錄
            bool attendCheck = db.ActivityLog.AsNoTracking().Any(x => x.UserId == userId && x.ActivityId == id && x.OrderState == true);
            if (!attendCheck) return Ok(new ApiMessageJwtResult { Status = false, JwtToken = jwtToken, Message = "未報名此活動" });

            // 回傳 JwtToken
            return Ok(new ApiMessageJwtResult
            {
                Status = true,
                JwtToken = jwtToken,
                Message = "已報名此活動"
            });
        }

        // GET: api/users/activity/collect/status/活動ID
        /// <summary>
        /// 5-5 確認是否已收藏活動 (JWT)
        /// </summary>
        /// <param name="id">活動ID</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpGet]
        [SwaggerResponse(typeof(ApiMessageJwtResult))]
        [Route("api/users/activity/collect/status/{id}")]
        public IHttpActionResult GetCollectStatus(int id)
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];
            // 查詢報名紀錄
            bool attendCheck = db.ActivityCollect.AsNoTracking().Any(x => x.UserId == userId && x.ActivityId == id);
            if (!attendCheck) return Ok(new ApiMessageJwtResult { Status = false, JwtToken = jwtToken, Message = "未收藏此活動" });

            // 回傳 JwtToken
            return Ok(new ApiMessageJwtResult
            {
                Status = true,
                JwtToken = jwtToken,
                Message = "已收藏此活動"
            });
        }

        // POST: api/users/attend-data
        /// <summary>
        /// 6-1 報名活動-個資帶入 (JWT)
        /// </summary>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPost]
        [SwaggerResponse(typeof(UserRealDataJwtVm))]
        [Route("api/users/attend-data")]
        public IHttpActionResult GetUserRealData()
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            //單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            var idData = (int)userToken["Id"];

            // 查詢指定帳號
            var userQuery = db.User.AsNoTracking().FirstOrDefault(x => x.Id == idData);

            // 回傳 JwtToken 及姓名+手機
            return Ok(new UserRealDataJwtVm
            {
                Status = true,
                JwtToken = jwtToken,
                Data = new UserRealData
                {
                    Id = userQuery.Id,
                    Name = userQuery.Name,
                    MobilePhone = userQuery.MobilePhone,
                    Account = userToken["Account"].ToString()
                }
            });
        }

        // POST: api/users/activity/free/attend
        /// <summary>
        /// 6-2 報名活動-免費+發信 (JWT)
        /// </summary>
        /// <param name="attendData">活動報名請求資料</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPost]
        [SwaggerResponse(typeof(ActivityFreeAttendJwtVm))]
        [Route("api/users/activity/free/attend")]
        public IHttpActionResult AttendFreeActivity(ActivityRequestVm attendData)
        {
            // 必填欄位資料檢查
            if (!ModelState.IsValid) return Ok(new ApiMessageResult { Status = false, Message = "未填欄位" });
            if (attendData == null) return Ok(new ApiMessageResult { Status = false, Message = "未填欄位" });
            else if (attendData.ActivityPrice > 0) return Ok(new ApiMessageResult { Status = false, Message = "非免費活動" });

            // 手機格式檢查
            if (!Mail.RegexMobilePhoneTW(attendData.UserMobilePhone)) return Ok(new ApiMessageResult { Status = false, Message = "手機格式錯誤" });

            // 查詢指定活動
            var activityQuery = db.Activity.FirstOrDefault(x => x.Id == attendData.ActivityId);
            if (activityQuery.ApplicantFull) return Ok(new ApiMessageResult { Status = false, Message = "報名額滿" });
            else if (!(activityQuery.FreeCost)) return Ok(new ApiMessageResult { Status = false, Message = "非免費活動" });
            else if (activityQuery.EndAcceptDate < DateTime.Now) return Ok(new ApiMessageResult { Status = false, Message = "截止報名" });
            else if (activityQuery == null) return Ok(new ApiMessageResult { Status = false, Message = "活動不存在" });

            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            //單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            // JwtToken 不同，需重新登入
            if (!(attendData.UserId == (int)userToken["Id"])) return Ok(new ApiMessageResult { Status = false, Message = "請重新登入" });

            try {
                // 查詢報名紀錄
                var activityLogQuery = db.ActivityLog.Where(x => x.UserId == attendData.UserId && x.ActivityId == attendData.ActivityId);
                if (activityLogQuery.Any(x => x.OrderState == true)) return Ok(new ApiMessageResult { Status = false, Message = "重複報名" });
                // 已取消又再報名
                else if (activityLogQuery.Any(x => x.OrderState == false)) {
                    activityLogQuery.FirstOrDefault().OrderState = true;
                }
                // 新報名
                else {
                    ActivityLog activityLogInput = new ActivityLog
                    {
                        ActivityId = attendData.ActivityId,
                        ActivityPrice = 0,
                        OrderState = true,
                        UserId = attendData.UserId,
                        UserName = attendData.UserName,
                        UserMobilePhone = attendData.UserMobilePhone,
                        CreatDate = DateTime.Now
                    };
                    // 最後一位報名時修改活動為額滿狀態
                    activityQuery.ApplicantNumber = db.ActivityLog.Count(x => x.ActivityId == attendData.ActivityId);
                    if (activityQuery.LimitNumber == activityQuery.ApplicantNumber + 1) {
                        activityQuery.ApplicantNumber += 1;
                        db.ActivityLog.Add(activityLogInput);
                        // 存檔前額滿二次確認
                        if (activityQuery.ApplicantFull) return Ok(new ApiMessageResult { Status = false, Message = "報名額滿" });
                        else {
                            // 活動改為額滿並儲存報名資料
                            activityQuery.ApplicantFull = true;
                            db.SaveChanges();
                        };
                    }
                    // 非最後一位報名，報名人數+1並儲存
                    activityQuery.ApplicantNumber += 1;
                    db.ActivityLog.Add(activityLogInput);
                }
                db.SaveChanges();

                // 寄出報名成功通知信
                string onLineLink = String.IsNullOrEmpty(activityQuery.Link) ? "" : $"線上活動連結 : {activityQuery.Link}";
                string htmlBody =
                    "<h1>作伙來讀冊</h1>" +
                    $"<h3>{attendData.UserName} 您好,</h3>" +
                    $"<h3>非常感謝你於平台報名{activityQuery.Name} {activityQuery.ActivityType}</h3>" +
                    $"<h3>活動開始時間 : {activityQuery.ActivityStartDate.ToString("g")}</h3>" +
                    $"<h3>活動結束時間 : {activityQuery.ActivityEndDate.ToString("g")}</h3>" +
                    $"<h3>{onLineLink}</h3>" +
                    $"<h3>*主辦單位將於活動開始前開放入場，並會與你核對報名身分!</h3>" +
                    $"<h3>*請於活動開始時準時參加，建議將活動時間存入個人行事曆!</h3>" +
                    $"<h3>***提醒事項***</h3>" +
                    $"<p>-煩請各位參加者事先預留活動之時間，希望大家可以好好享受活動的樂趣。</p>";
                Mail.SendActivityAttendMail(attendData.UserName, attendData.UserAccount, htmlBody);
            }
            catch (Exception ex) {

                return Ok(new ApiMessageResult { Status = false, Message = ex.Message });
            }

            // 刷新 JwtToken 並送出活動資料
            return Ok(new ActivityFreeAttendJwtVm
            {
                Status = true,
                JwtToken = jwtToken,
                Data = new ActivityAttendData
                {
                    ActivityId = attendData.ActivityId,
                    ActivityName = activityQuery.Name,
                    ActivityType = activityQuery.ActivityType,
                    ActivityStartDate = activityQuery.ActivityStartDate,
                    ActivityEndDate = activityQuery.ActivityEndDate
                }
            });
        }

        // POST: api/users/activity/charge/attend
        /// <summary>
        /// 6-3 確認報名-付費活動-付款前 (JWT)
        /// </summary>
        /// <param name="attendData">活動報名請求資料</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPost]
        [SwaggerResponse(typeof(PaymentSubmitJwtVm))]
        [Route("api/users/activity/charge/attend")]
        public IHttpActionResult AttendChargeActivity(ActivityRequestVm attendData)
        {
            // 必填欄位資料檢查
            if (!ModelState.IsValid) return Ok(new ApiMessageResult { Status = false, Message = "未填欄位" });
            if (attendData == null) return Ok(new ApiMessageResult { Status = false, Message = "未填欄位" });
            else if (attendData.ActivityPrice < 0) return Ok(new ApiMessageResult { Status = false, Message = "是免費活動" });

            // 手機格式檢查
            if (!Mail.RegexMobilePhoneTW(attendData.UserMobilePhone)) return Ok(new ApiMessageResult { Status = false, Message = "手機格式錯誤" });

            // 查詢報名紀錄
            var activityLogQuery = db.ActivityLog.Where(x => x.UserId == attendData.UserId && x.ActivityId == attendData.ActivityId);
            if (activityLogQuery.Any(x => x.OrderState.Value)) return Ok(new ApiMessageResult { Status = false, Message = "重複報名" });

            // 查詢指定活動
            var activityQuery = db.Activity.FirstOrDefault(x => x.Id == attendData.ActivityId);
            if (activityQuery.ApplicantFull) return Ok(new ApiMessageResult { Status = false, Message = "報名額滿" });
            else if (activityQuery.FreeCost) return Ok(new ApiMessageResult { Status = false, Message = "是免費活動" });
            else if (activityQuery.EndAcceptDate < DateTime.Now) return Ok(new ApiMessageResult { Status = false, Message = "截止報名" });
            else if (activityQuery == null) return Ok(new ApiMessageResult { Status = false, Message = "活動不存在" });

            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            //單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];
            // JwtToken 不同，需重新登入
            if (!(attendData.UserId == userId)) return Ok(new ApiMessageResult { Status = false, Message = "請重新登入" });

            DateTime dateTimeNow = DateTime.Now;
            try {
                if (activityLogQuery.Count() == 0) {
                    // 新報名生成活動報名紀錄
                    ActivityLog activityLogInput = new ActivityLog
                    {
                        ActivityId = attendData.ActivityId,
                        ActivityPrice = activityQuery.Price,
                        OrderState = false, // 付款完成前先設成false
                        UserId = attendData.UserId,
                        UserName = attendData.UserName,
                        UserMobilePhone = attendData.UserMobilePhone,
                        CreatDate = dateTimeNow
                    };

                    // 最後一位報名時修改活動為額滿狀態
                    activityQuery.ApplicantNumber = db.ActivityLog.Count(x => x.ActivityId == attendData.ActivityId);
                    if (activityQuery.LimitNumber == activityQuery.ApplicantNumber + 1) {
                        activityQuery.ApplicantNumber += 1;
                        db.ActivityLog.Add(activityLogInput);
                        // 存檔前額滿二次確認
                        if (activityQuery.ApplicantFull) return Ok(new ApiMessageResult { Status = false, Message = "報名額滿" });
                        else {
                            // 活動改為額滿並儲存報名資料
                            activityQuery.ApplicantFull = true;
                            db.SaveChanges();
                        };

                    }
                    // 非最後一位報名，報名人數+1並儲存
                    activityQuery.ApplicantNumber += 1;
                    db.ActivityLog.Add(activityLogInput);
                    db.SaveChanges();
                }
            }
            catch (Exception ex) {

                return Ok(new ApiMessageResult { Status = false, Message = ex.Message });
            }

            // 查詢此筆訂單資料
            var orderQuery = db.ActivityLog.FirstOrDefault(x => x.ActivityId == attendData.ActivityId && x.UserId == userId);

            // 已報名未付款，整理金流串接資料
            // 加密用金鑰
            string hashKey = WebConfigurationManager.AppSettings["HashKey"];
            string hashIV = WebConfigurationManager.AppSettings["HashIV"];

            // 金流接收資料
            string merchantID = WebConfigurationManager.AppSettings["MerchantID"];
            string tradeInfo = "";
            string tradeSha = "";
            string version = WebConfigurationManager.AppSettings["Version"];

            // tradeInfo 內容
            string respondType = "JSON"; // 回傳格式
            string timeStamp = ((int)(dateTimeNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds).ToString();
            string merchantOrderNo = timeStamp +"_"+ orderQuery.Id.ToString(); // 底線後方為訂單ID，解密比對用
            string amt = activityQuery.Price.ToString(); // 訂單金額
            string itemDesc = activityQuery.ActivityType.ToString(); // 商品資訊
            string tradeLimit = "600"; // 交易限制秒數
            string notifyURL = @"https://" + Request.RequestUri.Host + WebConfigurationManager.AppSettings["NotifyURL"]; //後端 API 接收藍新付款結果
            string returnURL = WebConfigurationManager.AppSettings["ReturnURL"] + activityQuery.Id; // 前端可用 Status: SUCCESS 來判斷付款成功，網址夾帶可拿來取得活動內容
            string email = userToken["Account"].ToString(); // 通知付款完成用
            string loginType = "0"; // 0不須登入藍新金流會員

            // 將 model 轉換為List<KeyValuePair<string, string>>
            List<KeyValuePair<string, string>> tradeData = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("MerchantID", merchantID),
                new KeyValuePair<string, string>("RespondType", respondType),
                new KeyValuePair<string, string>("TimeStamp", timeStamp),
                new KeyValuePair<string, string>("Version", version),
                new KeyValuePair<string, string>("MerchantOrderNo", merchantOrderNo),
                new KeyValuePair<string, string>("Amt", amt),
                new KeyValuePair<string, string>("ItemDesc", itemDesc),
                new KeyValuePair<string, string>("TradeLimit", tradeLimit),
                new KeyValuePair<string, string>("NotifyURL", notifyURL),
                new KeyValuePair<string, string>("ReturnURL", returnURL),
                new KeyValuePair<string, string>("Email", email),
                new KeyValuePair<string, string>("LoginType", loginType)
            };

            // 將 List<KeyValuePair<string, string>> 轉換為 key1=Value1&key2=Value2&key3=Value3...
            var tradeQueryPara = string.Join("&", tradeData.Select(x => $"{x.Key}={x.Value}"));
            // AES 加密
            tradeInfo = CryptoUtil.EncryptAESHex(tradeQueryPara, hashKey, hashIV);
            // SHA256 加密 (格式參考文件)
            tradeSha = CryptoUtil.EncryptSHA256($"HashKey={hashKey}&{tradeInfo}&HashIV={hashIV}");

            // 送出金流串接用資料給前端送出用
            return Ok(new PaymentSubmitJwtVm
            {
                Status = true,
                JwtToken = jwtToken,
                PaymentData = new PaymentSubmitData
                {
                    MerchantID = merchantID,
                    TradeInfo = tradeInfo,
                    TradeSha = tradeSha,
                    Version = version
                }
            });
        }

        // POST: api/users/activity/payment/result
        /// <summary>
        /// *6-4 接收付款資訊+報名完成發信 (串接藍新-付款完)
        /// </summary>
        /// <param name="data">藍新金流資料</param>
        /// <returns></returns>
        [HttpPost]
        [OpenApiIgnore]
        [Route("api/users/activity/payment/result")]
        //[EnableCors("https://ccore.newebpay.com", "*", "*")] // 啟用跨網域存取
        public HttpResponseMessage GetPaymentData(NewebPayReturn data)
        {
            // 付款失敗
            var response = Request.CreateResponse(HttpStatusCode.OK);
            if (!data.Status.Equals("SUCCESS")) return response;

            // 加密用金鑰
            string hashKey = WebConfigurationManager.AppSettings["HashKey"];
            string hashIV = WebConfigurationManager.AppSettings["HashIV"];
            // AES 解密
            string decryptTradeInfo = CryptoUtil.DecryptAESHex(data.TradeInfo, hashKey, hashIV);
            PaymentResult result = JsonConvert.DeserializeObject<PaymentResult>(decryptTradeInfo);
            // 取出交易記錄資料庫ID
            string[] orderNo = result.Result.MerchantOrderNo.Split('_');
            int logId = Convert.ToInt32(orderNo[1]);

            // 修改訂單狀態為true
            var orderLogQuery = db.ActivityLog.FirstOrDefault(x => x.Id == logId);
            orderLogQuery.OrderState = true;
            db.SaveChanges();

            // 查詢指定活動
            var activityQuery = db.Activity.FirstOrDefault(x => x.Id == orderLogQuery.ActivityId);
            // 寄出報名成功通知信
            string onLineLink = String.IsNullOrEmpty(activityQuery.Link) ? "" : $"線上活動連結 : {activityQuery.Link}";
            string htmlBody =
                "<h1>作伙來讀冊</h1>" +
                $"<h3>{orderLogQuery.UserName} 您好,</h3>" +
                $"<h3>非常感謝你於平台報名{activityQuery.Name} {activityQuery.ActivityType}</h3>" +
                $"<h3>活動開始時間 : {activityQuery.ActivityStartDate.ToString("g")}</h3>" +
                $"<h3>活動結束時間 : {activityQuery.ActivityEndDate.ToString("g")}</h3>" +
                $"<h3>{onLineLink}</h3>" +
                $"<h3>*主辦單位將於活動開始前開放入場，並會與你核對報名身分!</h3>" +
                $"<h3>*請於活動開始時準時參加，建議將活動時間存入個人行事曆!</h3>" +
                $"<h3>***提醒事項***</h3>" +
                $"<p>-煩請各位參加者事先預留活動之時間，希望大家可以好好享受活動的樂趣。</p>" +
                $"<p>-活動開始的前 10 日如需取消報名，需酌收票價 10% 手續費。</p>" +
                $"<p>-若在活動開始的 10 日內取消報名，恕無法退還所有報名費用。</p>";
            Mail.SendActivityAttendMail(orderLogQuery.UserName, orderLogQuery.User.Account, htmlBody);

            return response;
        }

        // GET: api/users/activity/attend/profile/status/會員ID
        /// <summary>
        /// 7-1 確認是否為本人及有無追蹤 (JWT)
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpGet]
        [SwaggerResponse(typeof(NotMeFollowingJwtVm))]
        [Route("api/users/activity/attend/profile/status/{id}")]
        public IHttpActionResult GetAttendProfileStatus(int id)
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            // 比對Token紀錄
            if (!(userId == id)) return Ok(new NotMeFollowingJwtVm
            {
                Status = false,
                JwtToken = jwtToken,
                Message = "非本人資料",
                Following = db.UserFollowers.AsNoTracking().Where(x => x.UserId == userId).Any(x => x.FollowingUserId == id)
            });

            // 回傳 JwtToken
            return Ok(new NotMeFollowingJwtVm
            {
                Status = true,
                JwtToken = jwtToken,
                Message = "是本人資料"
            });
        }

        // GET: api/users/profile/會員ID
        /// <summary>
        /// 7-2 個人資料
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(UserDetailDataVm))]
        [Route("api/users/profile/{id}")]
        public IHttpActionResult GetUserProfile(int id)
        {
            int followersNumber = db.UserFollowers.AsNoTracking().Count(y => y.FollowingUserId == id);
            var userData = db.User.AsNoTracking().Where(x => x.Id == id).Select(x => new UserDetailData
            {
                Id = x.Id,
                Account = x.Account,
                Image = x.Image,
                Name = x.Name,
                NickName = x.NickName,
                FollowersNumber = followersNumber,
                FollowingNumber = x.UserFollowers.Count(z => z.UserId == id),
                Country = x.Country,
                City = x.City,
                Area = x.Area,
                AboutMe = x.AboutMe,
                MySkill = x.MySkill,
                MyInterest = x.MyInterest,
                FacebookLink = x.FacebookLink,
                InstagramLink = x.InstagramLink,
                CreatDate = x.CreatDate,
                Views = x.Views,
                ShowComingActivity = x.ShowComingActivity,
                ShowCollectActivity = x.ShowCollectActivity,
                ShowFinishActivity = x.ShowFinishActivity,
                ShowFollowing = x.ShowFollowing,
                ShowFollowers = x.ShowFollowers
            }).FirstOrDefault();

            return Ok(new UserDetailDataVm { Status = true, Data = userData });
        }

        // PUT: api/users/profile/views/會員ID
        /// <summary>
        /// 7-3 增加個人頁瀏覽人數
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerResponse(typeof(ApiMessageResult))]
        [Route("api/users/profile/views/{id}")]
        public IHttpActionResult AddActivityViews(int id)
        {
            try {
                // 查詢指定帳號
                var userQuery = db.User.FirstOrDefault(x => x.Id == id);

                // 增加瀏覽次數
                userQuery.Views += 1;
                db.SaveChanges();
            }
            catch (Exception ex) {

                return Ok(new ApiMessageResult { Status = false, Message = ex.Message });
            }

            return Ok(new ApiMessageResult { Status = true, Message = "個人頁瀏覽次數增加成功" });
        }

        // Put: api/users/follow/someone
        /// <summary>
        /// 7-4 追蹤/取消追蹤功能 (JWT)
        /// </summary>
        /// <param name="userData">登入請求資料</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPut]
        [SwaggerResponse(typeof(ApiMessageJwtResult))]
        [Route("api/users/follow/someone")]
        public IHttpActionResult SetFollowing(LoginRequest userData)
        {
            if (userData.FollowingUserId <= 0) return Ok(new ApiMessageResult { Status = false, Message = "此會員不存在" });

            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            string messageStr = "會員追蹤成功";
            try {
                // 查詢指定活動收藏記錄
                var userFollowersQuery = db.UserFollowers.Where(x => x.UserId == userId);

                // 會員追蹤執行判斷
                if (userFollowersQuery.Any(x => x.FollowingUserId == userData.FollowingUserId)) {
                    // 已有追蹤記錄時刪除記錄
                    db.UserFollowers.Remove(userFollowersQuery.FirstOrDefault(x => x.FollowingUserId == userData.FollowingUserId));
                    db.SaveChanges();
                    messageStr = "取消會員追蹤成功";
                    return Ok(new ApiMessageJwtResult { Status = true, JwtToken = jwtToken, Message = messageStr });
                }

                // 沒有追蹤記錄時增加記錄
                UserFollowers userFollowers = new UserFollowers
                {
                    UserId = userId,
                    FollowingUserId = userData.FollowingUserId,
                    CreatDate = DateTime.Now
                };
                db.UserFollowers.Add(userFollowers);
                db.SaveChanges();
            }
            catch (Exception ex) {

                return Ok(new ApiMessageJwtResult { Status = false, JwtToken = jwtToken, Message = ex.Message });
            }

            return Ok(new ApiMessageJwtResult { Status = true, JwtToken = jwtToken, Message = messageStr });
        }

        // GET: api/users/following/list/會員ID/每頁幾筆/第幾頁
        /// <summary>
        /// 7-5 關注中的會員資料+分頁
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <param name="split">每頁幾筆</param>
        /// <param name="page">第幾頁</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(UserFollowingDataVm))]
        [Route("api/users/following/list/{id}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetUsersFollowingList(int id, int split = 7, int page = 1)
        {
            // Join查詢關注記錄
            var userJoinData = db.User.AsNoTracking().Join(db.UserFollowers, // 第一個參數為要加入的資料來源
            u => u.Id, // 主表要join的值
            f => f.FollowingUserId, // 次表要join的值
            (u, f) => new FollowingUserData// 代表將資料集合起來
            {
                Id = u.Id,
                Account = u.Account,
                Image = u.Image,
                Name = u.Name,
                NickName = u.NickName,
                FollowersNumber = db.UserFollowers.Count(y => y.FollowingUserId == u.Id),
                FollowingNumber = db.UserFollowers.Count(z => z.UserId == u.Id),
                AboutMe = u.AboutMe,
                FollowingData = f.CreatDate,
                UserId = f.UserId,
                FollowingUserId = f.FollowingUserId
            }).OrderBy(x => x.FollowingData).Where(f => f.UserId == id);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)userJoinData.Count() / split);

            // .Skip() 前一定要接 .OrderBy()
            return Ok(new UserFollowingDataVm
            {
                Status = true,
                Data = new UserFollowingData
                {
                    TotalPage = totalPageNumber,
                    Following = userJoinData.Skip(split * (page - 1)).Take(split).ToArray()
                }
            });
        }

        // GET: api/users/followers/list/會員ID/每頁幾筆/第幾頁
        /// <summary>
        /// 7-6 追蹤者的會員資料+分頁
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <param name="split">每頁幾筆</param>
        /// <param name="page">第幾頁</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(UserFollowersDataVm))]
        [Route("api/users/followers/list/{id}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetUsersFollowersList(int id, int split = 7, int page = 1)
        {
            // Join查詢關注記錄
            var userJoinData = db.User.AsNoTracking().Join(db.UserFollowers, // 第一個參數為要加入的資料來源
            u => u.Id, // 主表要join的值
            f => f.UserId, // 次表要join的值
            (u, f) => new FollowersUserData// 代表將資料集合起來
            {
                Id = u.Id,
                Account = u.Account,
                Image = u.Image,
                Name = u.Name,
                NickName = u.NickName,
                FollowersNumber = db.UserFollowers.Count(y => y.FollowingUserId == u.Id),
                FollowingNumber = db.UserFollowers.Count(z => z.UserId == u.Id),
                AboutMe = u.AboutMe,
                FollowingData = f.CreatDate,
                UserId = f.UserId,
                FollowingUserId = f.FollowingUserId
            }).OrderBy(x => x.FollowingData).Where(f => f.FollowingUserId == id);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)userJoinData.Count() / split);

            // .Skip() 前一定要接 .OrderBy()
            return Ok(new UserFollowersDataVm
            {
                Status = true,
                Data = new UserFollowersData
                {
                    TotalPage = totalPageNumber,
                    Followers = userJoinData.Skip(split * (page - 1)).Take(split).ToArray()
                }
            });
        }

        // GET: api/users/activity/attend/coming/會員ID/每頁幾筆/第幾頁
        /// <summary>
        /// 7-7 即將到臨活動資料+分頁
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <param name="split">每頁幾筆</param>
        /// <param name="page">第幾頁</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(UserActivityAttendDataVm))]
        [Route("api/users/activity/attend/coming/{id}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetActivityAttendComingInfo(int id, int split = 5, int page = 1)
        {
            // 查詢活動參加記錄
            var attendInfo = db.ActivityLog.AsNoTracking().Where(x => x.UserId == id && x.OrderState == true && x.Activity.ActivityStartDate > DateTime.Now).Select(x => new ActivityAttendDetail
            {
                Id = x.ActivityId,
                Name = x.Activity.Name,
                Image = x.Activity.Image,
                OrganizerName = x.Activity.OrganizerName,
                ActivityStartDate = x.Activity.ActivityStartDate,
                ActivityEndDate = x.Activity.ActivityEndDate,
                Summary = x.Activity.Summary,
                Views = x.Activity.Views,
                ApplicantNumber = x.Activity.ApplicantNumber,
                CollectNumber = x.Activity.CollectNumber,
                EvaluateStars = x.Activity.EvaluateStars,
                OpinionNumber = x.Activity.OpinionNumber,
                ActivityType = x.Activity.ActivityType,
                ActivityVenue = x.Activity.ActivityVenue,
                Software = x.Activity.Software,
                Price = x.Activity.Price,
                Link = x.Activity.Link
            }).OrderBy(x => x.ActivityStartDate);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)attendInfo.Count() / split);

            return Ok(new UserActivityAttendDataVm
            {
                Status = true,
                Data = new UserActivityAttendData
                {
                    TotalPage = totalPageNumber,
                    MyActivity = attendInfo.Skip(split * (page - 1)).Take(split).ToArray()
                }
            });
        }

        // GET: api/users/activity/attend/opinions/會員ID/每頁幾筆/第幾頁
        /// <summary>
        /// 7-8 尚未評價活動資料+分頁
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <param name="split">每頁幾筆</param>
        /// <param name="page">第幾頁</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(UserActivityAttendDataVm))]
        [Route("api/users/activity/attend/opinions/{id}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetActivityAttendOpinionsInfo(int id, int split = 9, int page = 1)
        {
            // 查詢活動參加記錄
            var attendInfo = db.ActivityLog.AsNoTracking().Where(x => x.UserId == id && x.OrderState == true && x.Activity.ActivityEndDate < DateTime.Now && !x.Activity.ActivityOpinion.Any(y => y.UserId == id && y.ActivityId == x.ActivityId)).Select(x => new ActivityAttendDetail
            {
                Id = x.ActivityId,
                Name = x.Activity.Name,
                Image = x.Activity.Image,
                OrganizerName = x.Activity.OrganizerName,
                ActivityStartDate = x.Activity.ActivityStartDate,
                ActivityEndDate = x.Activity.ActivityEndDate,
                Summary = x.Activity.Summary,
                Views = x.Activity.Views,
                ApplicantNumber = x.Activity.ApplicantNumber,
                CollectNumber = x.Activity.CollectNumber,
                EvaluateStars = x.Activity.EvaluateStars,
                OpinionNumber = x.Activity.OpinionNumber,
                ActivityType = x.Activity.ActivityType,
                ActivityVenue = x.Activity.ActivityVenue,
                Software = x.Activity.Software,
                Price = x.Activity.Price,
                Link = x.Activity.Link
            }).OrderBy(x => x.ActivityStartDate);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)attendInfo.Count() / split);

            return Ok(new UserActivityAttendDataVm
            {
                Status = true,
                Data = new UserActivityAttendData
                {
                    TotalPage = totalPageNumber,
                    MyActivity = attendInfo.Skip(split * (page - 1)).Take(split).ToArray()
                }
            });
        }

        // GET: api/users/activity/info/collect/會員ID/每頁幾筆/第幾頁
        /// <summary>
        /// 7-9 收藏活動資料+分頁
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <param name="split">每頁幾筆</param>
        /// <param name="page">第幾頁</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(UserActivityAttendDataVm))]
        [Route("api/users/activity/info/collect/{id}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetActivityCollectInfo(int id, int split = 9, int page = 1)
        {
            // 查詢活動參加記錄
            var attendInfo = db.ActivityCollect.AsNoTracking().Where(x => x.UserId == id ).Select(x => new ActivityAttendDetail
            {
                Id = x.ActivityId,
                Name = x.Activity.Name,
                Image = x.Activity.Image,
                OrganizerName = x.Activity.OrganizerName,
                ActivityStartDate = x.Activity.ActivityStartDate,
                ActivityEndDate = x.Activity.ActivityEndDate,
                Summary = x.Activity.Summary,
                Views = x.Activity.Views,
                ApplicantNumber = x.Activity.ApplicantNumber,
                CollectNumber = x.Activity.CollectNumber,
                EvaluateStars = x.Activity.EvaluateStars,
                OpinionNumber = x.Activity.OpinionNumber,
                ActivityType = x.Activity.ActivityType,
                ActivityVenue = x.Activity.ActivityVenue,
                Software = x.Activity.Software,
                Price = x.Activity.Price,
                Link = x.Activity.Link
            }).OrderBy(x => x.ActivityStartDate);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)attendInfo.Count() / split);

            return Ok(new UserActivityAttendDataVm
            {
                Status = true,
                Data = new UserActivityAttendData
                {
                    TotalPage = totalPageNumber,
                    MyActivity = attendInfo.Skip(split * (page - 1)).Take(split).ToArray()
                }
            });
        }

        // GET: api/users/activity/attend/done/會員ID/每頁幾筆/第幾頁
        /// <summary>
        /// 7-10 完成活動資料+分頁
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <param name="split">每頁幾筆</param>
        /// <param name="page">第幾頁</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(UserActivityAttendDoneDataVm))]
        [Route("api/users/activity/attend/done/{id}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetActivityAttendDoneInfo(int id, int split = 9, int page = 1)
        {
            // 查詢活動參加記錄
            var attendInfo = db.ActivityLog.AsNoTracking().Where(x => x.UserId == id && x.OrderState == true && x.Activity.ActivityEndDate < DateTime.Now).Select(x => new ActivityAttendDoneDetail
            {
                Id = x.ActivityId,
                Name = x.Activity.Name,
                Image = x.Activity.Image,
                OrganizerName = x.Activity.OrganizerName,
                ActivityStartDate = x.Activity.ActivityStartDate,
                ActivityEndDate = x.Activity.ActivityEndDate,
                Summary = x.Activity.Summary,
                Views = x.Activity.Views,
                ApplicantNumber = x.Activity.ApplicantNumber,
                CollectNumber = x.Activity.CollectNumber,
                EvaluateStars = x.Activity.EvaluateStars,
                OpinionNumber = x.Activity.OpinionNumber,
                ActivityType = x.Activity.ActivityType,
                ActivityVenue = x.Activity.ActivityVenue,
                Software = x.Activity.Software,
                Price = x.Activity.Price,
                Link = x.Activity.Link,
                OpinionsDone = x.Activity.ActivityOpinion.Any(y => y.UserId == id && y.ActivityId == x.ActivityId)
            }).OrderBy(x => x.ActivityStartDate);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)attendInfo.Count() / split);

            return Ok(new UserActivityAttendDoneDataVm
            {
                Status = true,
                Data = new UserActivityAttendDoneData
                {
                    TotalPage = totalPageNumber,
                    MyActivity = attendInfo.Skip(split * (page - 1)).Take(split).ToArray()
                }
            });
        }

        // GET: api/users/activity/attend/cancel/會員ID/每頁幾筆/第幾頁
        /// <summary>
        /// 7-11 取消活動資料+分頁
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <param name="split">每頁幾筆</param>
        /// <param name="page">第幾頁</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(UserActivityAttendDataVm))]
        [Route("api/users/activity/attend/cancel/{id}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetActivityAttendCancelInfo(int id, int split = 9, int page = 1)
        {
            // 查詢活動參加記錄
            var attendInfo = db.ActivityLog.AsNoTracking().Where(x => x.UserId == id && x.OrderState == false).Select(x => new ActivityCancelDetail
            {
                Id = x.ActivityId,
                Name = x.Activity.Name,
                Image = x.Activity.Image,
                OrganizerName = x.Activity.OrganizerName,
                ActivityStartDate = x.Activity.ActivityStartDate,
                ActivityEndDate = x.Activity.ActivityEndDate,
                Summary = x.Activity.Summary,
                Views = x.Activity.Views,
                ApplicantNumber = x.Activity.ApplicantNumber,
                CollectNumber = x.Activity.CollectNumber,
                EvaluateStars = x.Activity.EvaluateStars,
                OpinionNumber = x.Activity.OpinionNumber,
                ActivityType = x.Activity.ActivityType,
                ActivityVenue = x.Activity.ActivityVenue,
                Software = x.Activity.Software,
                Price = x.Activity.Price
            }).OrderBy(x => x.ActivityStartDate);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)attendInfo.Count() / split);

            return Ok(new UserActivityCancelDataVm
            {
                Status = true,
                Data = new UserActivityCancelData
                {
                    TotalPage = totalPageNumber,
                    MyActivity = attendInfo.Skip(split * (page - 1)).Take(split).ToArray()
                }
            });
        }

        // Put: api/users/activity/attend/cancel/state
        /// <summary>
        /// 7-12 取消報名功能 (JWT)
        /// </summary>
        /// <param name="logData">活動訂單紀錄</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPut]
        [SwaggerResponse(typeof(ApiMessageJwtResult))]
        [Route("api/users/activity/attend/cancel/state")]
        public IHttpActionResult SetActivityAttendCancel(ActivityCancelVm logData)
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            try {
                // 查詢指定活動
                var activityQuery = db.Activity.FirstOrDefault(x => x.Id == logData.ActivityId);
                // 查詢活動報名記錄
                var logQuery = db.ActivityLog.Where(x => x.ActivityId == logData.ActivityId && x.UserId == userId && x.OrderState == true).FirstOrDefault();
                // 會員報名狀態判斷
                if (logQuery != null) {
                    logQuery.OrderState = false;
                    activityQuery.ApplicantNumber -= 1;
                    db.SaveChanges();
                }
                else {
                    return Ok(new ApiMessageJwtResult { Status = true, JwtToken = jwtToken, Message = "未報名此活動" });
                }
            }
            catch (Exception ex) {

                return Ok(new ApiMessageJwtResult { Status = false, JwtToken = jwtToken, Message = ex.Message });
            }

            return Ok(new ApiMessageJwtResult { Status = true, JwtToken = jwtToken, Message = "取消活動成功" });
        }

        // POST: api/users/activity/attend/opinion
        /// <summary>
        /// 7-13 填寫活動評價 (JWT)
        /// </summary>
        /// <param name="opinionData">活動評價紀錄</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPost]
        [SwaggerResponse(typeof(ApiMessageJwtResult))]
        [Route("api/users/activity/attend/opinion")]
        public IHttpActionResult SetAttendOpinion(ActivityOpinionVm opinionData)
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            // 檢查欄位
            if (opinionData == null) return Ok(new ApiMessageJwtResult { Status = false, JwtToken = jwtToken, Message = "未填欄位" });
            if (!(opinionData.ActivityId > 0)) return Ok(new ApiMessageJwtResult { Status = false, JwtToken = jwtToken, Message = "未提供活動ID" });
            if (!(opinionData.Star > 0)) return Ok(new ApiMessageJwtResult { Status = false, JwtToken = jwtToken, Message = "未填星星數" });
            if (opinionData.Opinion == null) return Ok(new ApiMessageJwtResult { Status = false, JwtToken = jwtToken, Message = "未填評價" });
            if (opinionData.Opinion.Length >= 200) return Ok(new ApiMessageJwtResult { Status = false, JwtToken = jwtToken, Message = "超出字數限制" });

            try {
                // 查詢評價紀錄
                int userId = (int)userToken["Id"];
                bool attendCheck = db.ActivityOpinion.Any(x => x.UserId == userId && x.ActivityId == opinionData.ActivityId);
                if (attendCheck) return Ok(new ApiMessageJwtResult { Status = false, JwtToken = jwtToken, Message = "已評價" });

                // 新增評價
                ActivityOpinion userInput = new ActivityOpinion
                {
                    UserId = userId,
                    ActivityId = opinionData.ActivityId,
                    Star = opinionData.Star,
                    Opinion = opinionData.Opinion,
                    CreatDate = DateTime.Now
                };
                db.ActivityOpinion.Add(userInput);
                // 刷新活動平均星星數 (無條件進位)
                var opinionQuery = db.ActivityOpinion.Where(x => x.ActivityId == opinionData.ActivityId);
                float sumStar = opinionQuery.Sum(x => x.Star) + opinionData.Star;
                float countStarList = opinionQuery.Count() + 1;
                // 更新活動數值
                var activityQuery = db.Activity.FirstOrDefault(x => x.Id == opinionData.ActivityId);
                activityQuery.EvaluateStars = ((int)Math.Ceiling(sumStar / countStarList));
                activityQuery.OpinionNumber += 1;
                db.SaveChanges();
            }
            catch (Exception ex) {

                return Ok(new ApiMessageJwtResult { Status = false, JwtToken = jwtToken, Message = ex.Message });
            }

            return Ok(new ApiMessageJwtResult
            {
                Status = true,
                JwtToken = jwtToken,
                Message = "評價活動成功"
            });
        }

        // Put: api/users/profile/edit
        /// <summary>
        /// 7-14 編輯個人檔案 (JWT)
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPut]
        [SwaggerResponse(typeof(ApiMessageJwtResult))]
        [Route("api/users/profile/edit")]
        public IHttpActionResult SetProfileEditData(UserEditDataVm userData)
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            // 檢查欄位
            if (userData == null) return Ok(new ApiMessageJwtResult { Status = false, JwtToken = jwtToken, Message = "未填欄位" });

            try {
                // 查詢指定活動收藏記錄
                var userQuery = db.User.FirstOrDefault(x => x.Id == userId);
                // 設定更新值
                userQuery.Name = userData.Name;
                userQuery.NickName = userData.NickName;
                userQuery.BirthDate = userData.BirthDate;
                userQuery.Sex = userData.Sex;
                userQuery.MobilePhone = userData.MobilePhone;
                userQuery.Country = userData.Country;
                userQuery.City = userData.City;
                userQuery.Area = userData.Area;
                userQuery.FacebookLink = userData.FacebookLink;
                userQuery.InstagramLink = userData.InstagramLink;
                userQuery.AboutMe = userData.AboutMe;
                userQuery.MySkill = userData.MySkill;
                userQuery.MyInterest = userData.MyInterest;
                userQuery.ShowComingActivity = userData.ShowComingActivity;
                userQuery.ShowCollectActivity = userData.ShowCollectActivity;
                userQuery.ShowFinishActivity = userData.ShowFinishActivity;
                userQuery.ShowCancelActivity = userData.ShowCancelActivity;
                userQuery.ShowFollowing = userData.ShowFollowing;
                userQuery.ShowFollowers = userData.ShowFollowers;
                db.SaveChanges();
            }
            catch (Exception ex) {

                return Ok(new ApiMessageJwtResult { Status = false, JwtToken = jwtToken, Message = ex.Message });
            }

            return Ok(new ApiMessageJwtResult { Status = true, JwtToken = jwtToken, Message = "更新編輯內容成功" });
        }

        // GET: api/users/login/following/list/會員ID/每頁幾筆/第幾頁
        /// <summary>
        /// 7-15 關注中的會員資料+分頁 (JWT)
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <param name="split">每頁幾筆</param>
        /// <param name="page">第幾頁</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpGet]
        [SwaggerResponse(typeof(UserFollowingDataJwtVm))]
        [Route("api/users/login/following/list/{id}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetLoginUsersFollowingList(int id, int split = 7, int page = 1)
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            // Join查詢關注記錄
            var userJoinData = db.User.AsNoTracking().Join(db.UserFollowers, // 第一個參數為要加入的資料來源
            u => u.Id, // 主表要join的值
            f => f.FollowingUserId, // 次表要join的值
            (u, f) => new FollowingUserDataJWT// 代表將資料集合起來
            {
                Id = u.Id,
                Account = u.Account,
                Image = u.Image,
                Name = u.Name,
                NickName = u.NickName,
                FollowersNumber = db.UserFollowers.Count(y => y.FollowingUserId == u.Id),
                FollowingNumber = db.UserFollowers.Count(z => z.UserId == u.Id),
                AboutMe = u.AboutMe,
                FollowingData = f.CreatDate,
                UserId = f.UserId,
                FollowingUserId = f.FollowingUserId,
                Following = db.UserFollowers.Any(g => g.UserId == userId && g.FollowingUserId == u.Id)
            }).OrderBy(x => x.FollowingData).Where(f => f.UserId == id);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)userJoinData.Count() / split);

            // .Skip() 前一定要接 .OrderBy()
            // 刷新 JwtToken 並送出活動資料
            return Ok(new UserFollowingDataJwtVm
            {
                Status = true,
                JwtToken = jwtToken,
                Data = new UserFollowingDataJWT
                {
                    TotalPage = totalPageNumber,
                    Following = userJoinData.Skip(split * (page - 1)).Take(split).ToArray()
                }
            });
        }

        // GET: api/users/login/followers/list/會員ID/每頁幾筆/第幾頁
        /// <summary>
        /// 7-16 追蹤者的會員資料+分頁 (JWT)
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <param name="split">每頁幾筆</param>
        /// <param name="page">第幾頁</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpGet]
        [SwaggerResponse(typeof(UserFollowersDataJwtVm))]
        [Route("api/users/login/followers/list/{id}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetLoginUsersFollowersList(int id, int split = 7, int page = 1)
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            // Join查詢關注記錄
            var userJoinData = db.User.AsNoTracking().Join(db.UserFollowers, // 第一個參數為要加入的資料來源
            u => u.Id, // 主表要join的值
            f => f.UserId, // 次表要join的值
            (u, f) => new FollowersUserDataJWT // 代表將資料集合起來
            {
                Id = u.Id,
                Account = u.Account,
                Image = u.Image,
                Name = u.Name,
                NickName = u.NickName,
                FollowersNumber = db.UserFollowers.Count(y => y.FollowingUserId == u.Id),
                FollowingNumber = db.UserFollowers.Count(z => z.UserId == u.Id),
                AboutMe = u.AboutMe,
                FollowingData = f.CreatDate,
                UserId = f.UserId,
                FollowingUserId = f.FollowingUserId,
                Following = db.UserFollowers.Any(g => g.UserId == userId && g.FollowingUserId == u.Id)
            }).OrderBy(x => x.FollowingData).Where(f => f.FollowingUserId == id);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)userJoinData.Count() / split);

            // .Skip() 前一定要接 .OrderBy()
            // 刷新 JwtToken 並送出活動資料
            return Ok(new UserFollowersDataJwtVm
            {
                Status = true,
                JwtToken = jwtToken,
                Data = new UserFollowersDataJWT
                {
                    TotalPage = totalPageNumber,
                    Followers = userJoinData.Skip(split * (page - 1)).Take(split).ToArray()
                }
            });
        }

        // GET: api/users/profile/detail/會員ID
        /// <summary>
        /// 7-17 編輯頁個人資料 (JWT)
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpGet]
        [SwaggerResponse(typeof(UserEditDataJwtVm))]
        [Route("api/users/profile/detail/{id}")]
        public IHttpActionResult GetUserProfileDetail(int id)
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);

            int followersNumber = db.UserFollowers.AsNoTracking().Count(y => y.FollowingUserId == id);
            var userData = db.User.Where(x => x.Id == id).Select(x => new UserEditDataJwt
            {
                Id = x.Id,
                Account = x.Account,
                Image = x.Image,
                Name = x.Name,
                NickName = x.NickName,
                BirthDate = x.BirthDate,
                Sex = x.Sex,
                MobilePhone = x.MobilePhone,
                Country = x.Country,
                City = x.City,
                Area = x.Area,
                AboutMe = x.AboutMe,
                MySkill = x.MySkill,
                MyInterest = x.MyInterest,
                FacebookLink = x.FacebookLink,
                InstagramLink = x.InstagramLink,
                CreatDate = x.CreatDate,
                ShowComingActivity = x.ShowComingActivity,
                ShowCollectActivity = x.ShowCollectActivity,
                ShowFinishActivity = x.ShowFinishActivity,
                ShowFollowing = x.ShowFollowing,
                ShowFollowers = x.ShowFollowers
            }).FirstOrDefault();

            // 刷新 JwtToken 並送出活動資料
            return Ok(new UserEditDataJwtVm
            {
                Status = true,
                JwtToken = jwtToken,
                Data = userData
            });
        }

        // Post: api/users/profile/upload
        /// <summary>
        /// *7-18 上傳個人頭貼 (JWT)
        /// </summary>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPost]
        [SwaggerResponse(typeof(UploadProfileJwtVm))]
        [Route("api/users/profile/upload")]
        public async Task<IHttpActionResult> UploadProfile()
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);

            // 檢查請求是否包含 multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent()) {
                string messageJson = JsonConvert.SerializeObject(new { Status = false, Message = "multipart/form-data error" });
                var errorMessage = new HttpResponseMessage()
                {
                    ReasonPhrase = "multipart/form-data error",
                    Content = new StringContent(messageJson,
                                Encoding.UTF8,
                                "application/json")
                };
                throw new HttpResponseException(errorMessage);
            }

            // 檢查資料夾是否存在，若無則建立
            string root = HttpContext.Current.Server.MapPath("~/upload/profile");
            if (!Directory.Exists(root)) {
                Directory.CreateDirectory("~/upload/profile");
            }

            try {
                // 讀取 MIME 資料
                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);

                // 取得檔案副檔名，單檔用.FirstOrDefault()直接取出，多檔需用迴圈
                string fileNameData = provider.Contents.FirstOrDefault().Headers.ContentDisposition.FileName.Trim('\"');
                string fileType = fileNameData.Remove(0, fileNameData.LastIndexOf('.')); // .jpg

                // 定義檔案名稱
                string[] userAccountData = userToken["Account"].ToString().Split('@');
                string fileName = userAccountData[0] + "Profile" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileType;

                // 儲存圖片，單檔用.FirstOrDefault()直接取出，多檔需用迴圈
                var fileBytes = await provider.Contents.FirstOrDefault().ReadAsByteArrayAsync();
                var outputPath = Path.Combine(root, fileName);
                using (var output = new FileStream(outputPath, FileMode.Create, FileAccess.Write)) {
                    await output.WriteAsync(fileBytes, 0, fileBytes.Length);
                }

                // 使用 SixLabors.ImageSharp 調整圖片尺寸 (正方形大頭貼)
                var image = SixLabors.ImageSharp.Image.Load<Rgba32>(outputPath);
                image.Mutate(x => x.Resize(120, 120)); // 輸入(120, 0)會保持比例出現黑邊
                image.Save(outputPath);

                return Ok(new UploadProfileJwtVm
                {
                    Status = true,
                    JwtToken = jwtToken,
                    Data = new UploadProfileData
                    {
                        FileName = fileName
                    }
                });
            }
            catch (Exception ex) {

                return Ok(new { Status = false, Message = ex.Message });
            }
        }
    }
}