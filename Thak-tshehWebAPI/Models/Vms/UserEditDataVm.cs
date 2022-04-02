using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 會員編輯資料
    /// </summary>
    public class UserEditDataVm
    {
        /// <summary>
        /// 真實姓名
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "真實姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 會員暱稱
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "會員暱稱")]
        public string NickName { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [Display(Name = "出生日期")]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 性別
        /// </summary>
        [Display(Name = "性別")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Sex? Sex { get; set; }

        /// <summary>
        /// 手機號碼
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "手機號碼")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// 國家
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "國家")]
        public string Country { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "城市")]
        public string City { get; set; }

        /// <summary>
        /// 地區
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "地區")]
        public string Area { get; set; }

        /// <summary>
        /// Facebook連結
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "Facebook連結")]
        public string FacebookLink { get; set; }

        /// <summary>
        /// Instagram連結
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "Instagram連結")]
        public string InstagramLink { get; set; }

        /// <summary>
        /// 關於我
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "關於我")]
        public string AboutMe { get; set; }

        /// <summary>
        /// 我的專長
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "我的專長")]
        public string MySkill { get; set; }

        /// <summary>
        /// 我的興趣
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "我的興趣")]
        public string MyInterest { get; set; }

        /// <summary>
        /// 顯示即將到臨的活動
        /// </summary>
        [Display(Name = "顯示即將到臨的活動")]
        public bool ShowComingActivity { get; set; }

        /// <summary>
        /// 顯示尚未評價的活動
        /// </summary>
        [Display(Name = "顯示尚未評價的活動")]
        public bool ShowNeedEvalActivity { get; set; }

        /// <summary>
        /// 顯示收藏的活動
        /// </summary>
        [Display(Name = "顯示收藏的活動")]
        public bool ShowCollectActivity { get; set; }

        /// <summary>
        /// 顯示已完成的活動
        /// </summary>
        [Display(Name = "顯示已完成的活動")]
        public bool ShowFinishActivity { get; set; }

        /// <summary>
        /// 顯示取消的活動
        /// </summary>
        [Display(Name = "顯示取消的活動")]
        public bool ShowCancelActivity { get; set; }

        /// <summary>
        /// 顯示關注中的用戶
        /// </summary>
        [Display(Name = "顯示關注中的用戶")]
        public bool ShowFollowing { get; set; }

        /// <summary>
        /// 顯示追蹤者
        /// </summary>
        [Display(Name = "顯示追蹤者")]
        public bool ShowFollowers { get; set; }
    }
}