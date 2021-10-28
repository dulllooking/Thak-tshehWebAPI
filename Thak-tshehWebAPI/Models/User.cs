using Newtonsoft.Json;
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
        [JsonProperty("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "信箱帳號")]
        [JsonProperty("account")]
        public string Account { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "密碼")]
        [JsonProperty("hashPassword")]
        public string HashPassword { get; set; }

        [MaxLength(50)]
        [Display(Name = "雜湊鹽")]
        [JsonProperty("salt")]
        public string Salt { get; set; }

        [MaxLength(200)]
        [Display(Name = "頭貼圖檔名稱")]
        [JsonProperty("image")]
        public string Image { get; set; }

        [MaxLength(50)]
        [Display(Name = "姓名")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [MaxLength(50)]
        [Display(Name = "暱稱")]
        [JsonProperty("nickName")]
        public string NickName { get; set; }

        [MaxLength(50)]
        [Display(Name = "國家")]
        [JsonProperty("country")]
        public string Country { get; set; }

        [MaxLength(50)]
        [Display(Name = "城市")]
        [JsonProperty("city")]
        public string City { get; set; }

        [MaxLength(50)]
        [Display(Name = "地區")]
        [JsonProperty("area")]
        public string Area { get; set; }

        [MaxLength(200)]
        [Display(Name = "Facebook連結")]
        [JsonProperty("facebookLink")]
        public string FacebookLink { get; set; }

        [MaxLength(200)]
        [Display(Name = "Instagram連結")]
        [JsonProperty("instagramLink")]
        public string InstagramLink { get; set; }

        [MaxLength(200)]
        [Display(Name = "關於我")]
        [JsonProperty("aboutMe")]
        public string AboutMe { get; set; }

        [MaxLength(200)]
        [Display(Name = "我的專長")]
        [JsonProperty("mySkill")]
        public string MySkill { get; set; }

        [MaxLength(200)]
        [Display(Name = "我的興趣")]
        [JsonProperty("myInterest")]
        public string MyInterest { get; set; }

        [Display(Name = "顯示即將到臨的活動")]
        [JsonProperty("showWeekActivity")]
        public bool ShowWeekActivity { get; set; }

        [Display(Name = "顯示尚未評價的活動")]
        [JsonProperty("showNeedEvalActivity")]
        public bool ShowNeedEvalActivity { get; set; }

        [Display(Name = "顯示收藏的活動")]
        [JsonProperty("showCollectActivity")]
        public bool ShowCollectActivity { get; set; }

        [Display(Name = "顯示已完成的活動")]
        [JsonProperty("showFinishActivity")]
        public bool ShowFinishActivity { get; set; }

        [Display(Name = "顯示取消的活動")]
        [JsonProperty("showCancelActivity")]
        public bool ShowCancelActivity { get; set; }

        [Display(Name = "個人檔案瀏覽次數")]
        [JsonProperty("views")]
        public int Views { get; set; }

        [Display(Name = "帳號開通")]
        [JsonProperty("accountState")]
        public bool AccountState { get; set; }

        [Display(Name = "建立時間")]
        [JsonProperty("creatDate")]
        public DateTime CreatDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "信箱驗證碼")]
        [JsonProperty("checkMailCode")]
        public string CheckMailCode { get; set; }

        [Display(Name = "信箱驗證碼建立時間")]
        [JsonProperty("mailCodeCreatDate")]
        public DateTime? MailCodeCreatDate { get; set; }

        [MaxLength(50)]
        [Display(Name = "RefreshToken")]
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        [Display(Name = "建立時間")]
        [JsonProperty("refreshTokenCreatDate")]
        public DateTime? RefreshTokenCreatDate { get; set; }

        //阻止轉 Json 時的循環錯誤
        [JsonIgnore]
        public virtual ICollection<UserFollower> UserFollowers { get; set; }

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
        public virtual ICollection<TokenLog> TokenLog { get; set; }

        //阻止轉 Json 時的循環錯誤
        [JsonIgnore]
        public virtual ICollection<MailCode> MailCode { get; set; }
    }
}