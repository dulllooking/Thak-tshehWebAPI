using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Models
{
    /// <summary>
    /// 會員資料
    /// </summary>
    public class User
    {
        /// <summary>
        /// 會員編號
        /// </summary>
        [Key]
        [Display(Name = "會員編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 信箱帳號
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Display(Name = "信箱帳號")]
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Required]
        [MaxLength(100)]
        [Display(Name = "密碼")]
        public string HashPassword { get; set; }

        /// <summary>
        /// 雜湊鹽
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "雜湊鹽")]
        public string Salt { get; set; }

        /// <summary>
        /// 頭貼圖檔名稱
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "頭貼圖檔名稱")]
        public string Image { get; set; }

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

        /// <summary>
        /// 個人檔案瀏覽次數
        /// </summary>
        [Display(Name = "個人檔案瀏覽次數")]
        public int Views { get; set; }

        /// <summary>
        /// 帳號開通
        /// </summary>
        [Display(Name = "帳號開通")]
        public bool AccountState { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }

        /// <summary>
        /// 信箱驗證碼
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "信箱驗證碼")]
        public string CheckMailCode { get; set; }

        /// <summary>
        /// 信箱驗證碼到期時間
        /// </summary>
        [Display(Name = "信箱驗證碼到期時間")]
        public DateTime MailCodeCreatDate { get; set; }

        /// <summary>
        /// RefreshToken
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "RefreshToken")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// RefreshToken建立時間
        /// </summary>
        [Display(Name = "RefreshToken建立時間")]
        public DateTime? RefreshTokenCreatDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserFollowers> UserFollowers { get; set; }

        [JsonIgnore]
        public virtual ICollection<ActivityLog> ActivityLog { get; set; }

        [JsonIgnore]
        public virtual ICollection<ActivityCollect> ActivityCollect { get; set; }

        [JsonIgnore]
        public virtual ICollection<ActivityOpinion> ActivityOpinion { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrganizerLog> OrganizerLog { get; set; }

        [JsonIgnore]
        public virtual ICollection<TokenLog> TokenLog { get; set; }
    }
}