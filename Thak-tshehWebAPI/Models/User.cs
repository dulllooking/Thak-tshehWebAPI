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
    public class User
    {
        [Key]
        [Display(Name = "會員編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "信箱帳號")]
        public string Account { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "密碼")]
        public string HashPassword { get; set; }

        [MaxLength(50)]
        [Display(Name = "雜湊鹽")]
        public string Salt { get; set; }

        [MaxLength(200)]
        [Display(Name = "頭貼圖檔名稱")]
        public string Image { get; set; }

        [MaxLength(50)]
        [Display(Name = "真實姓名")]
        public string Name { get; set; }

        [MaxLength(50)]
        [Display(Name = "會員暱稱")]
        public string NickName { get; set; }

        [Display(Name = "出生日期")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "性別")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Sex? Sex { get; set; }

        [MaxLength(50)]
        [Display(Name = "手機號碼")]
        public string MobilePhone { get; set; }

        [MaxLength(50)]
        [Display(Name = "國家")]
        public string Country { get; set; }

        [MaxLength(50)]
        [Display(Name = "城市")]
        public string City { get; set; }

        [MaxLength(50)]
        [Display(Name = "地區")]
        public string Area { get; set; }

        [MaxLength(200)]
        [Display(Name = "Facebook連結")]
        public string FacebookLink { get; set; }

        [MaxLength(200)]
        [Display(Name = "Instagram連結")]
        public string InstagramLink { get; set; }

        [MaxLength(200)]
        [Display(Name = "關於我")]
        public string AboutMe { get; set; }

        [MaxLength(200)]
        [Display(Name = "我的專長")]
        public string MySkill { get; set; }

        [MaxLength(200)]
        [Display(Name = "我的興趣")]
        public string MyInterest { get; set; }

        [Display(Name = "顯示即將到臨的活動")]
        public bool ShowComingActivity { get; set; }

        [Display(Name = "顯示尚未評價的活動")]
        public bool ShowNeedEvalActivity { get; set; }

        [Display(Name = "顯示收藏的活動")]
        public bool ShowCollectActivity { get; set; }

        [Display(Name = "顯示已完成的活動")]
        public bool ShowFinishActivity { get; set; }

        [Display(Name = "顯示取消的活動")]
        public bool ShowCancelActivity { get; set; }

        [Display(Name = "顯示關注中的用戶")]
        public bool ShowFollowing { get; set; }

        [Display(Name = "顯示追蹤者")]
        public bool ShowFollowers { get; set; }

        [Display(Name = "個人檔案瀏覽次數")]
        public int Views { get; set; }

        [Display(Name = "帳號開通")]
        public bool AccountState { get; set; }

        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "信箱驗證碼")]
        public string CheckMailCode { get; set; }

        [Display(Name = "信箱驗證碼到期時間")]
        public DateTime MailCodeCreatDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "RefreshToken")]
        public string RefreshToken { get; set; }

        [Display(Name = "RefreshToken建立時間")]
        public DateTime? RefreshTokenCreatDate { get; set; }

        //阻止轉 Json 時的循環錯誤
        [JsonIgnore]
        public virtual ICollection<UserFollowers> UserFollowers { get; set; }

        //阻止轉 Json 時的循環錯誤
        [JsonIgnore]
        public virtual ICollection<ActivityLog> ActivityLog { get; set; }

        //阻止轉 Json 時的循環錯誤
        [JsonIgnore]
        public virtual ICollection<ActivityCollect> ActivityCollect { get; set; }

        //阻止轉 Json 時的循環錯誤
        [JsonIgnore]
        public virtual ICollection<ActivityOpinion> ActivityOpinion { get; set; }

        //阻止轉 Json 時的循環錯誤
        [JsonIgnore]
        public virtual ICollection<OrganizerLog> OrganizerLog { get; set; }

        //阻止轉 Json 時的循環錯誤
        [JsonIgnore]
        public virtual ICollection<TokenLog> TokenLog { get; set; }

    }
}