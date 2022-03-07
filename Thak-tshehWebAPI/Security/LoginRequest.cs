using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Security
{
    /// <summary>
    /// 登入請求資料
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 確認密碼
        /// </summary>
        public string CheckPassword { get; set; }

        /// <summary>
        /// 信箱驗證 GUID
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 活動 ID
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// 追蹤者 ID
        /// </summary>
        public int FollowingUserId { get; set; }

        public string guid { get; set; }
    }
}