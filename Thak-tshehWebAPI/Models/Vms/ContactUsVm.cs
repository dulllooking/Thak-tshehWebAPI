using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 留言資料
    /// </summary>
    public class ContactUsVm
    {
        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public string Message { get; set; }
    }
}