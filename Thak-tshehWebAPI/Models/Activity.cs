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
    public class Activity
    {
        [Key]
        [Display(Name = "活動編號")]
        [JsonProperty("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Display(Name = "活動名稱")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [MaxLength(200)]
        [Display(Name = "頭貼圖檔名稱")]
        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ActivityClass ActivityClass { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ActivityType ActivityType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ActivityVenue ActivityVenue { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Software Software { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Area Area { get; set; }

        [MaxLength(50)]
        [Display(Name = "地址")]
        [JsonProperty("address")]
        public string Address { get; set; }

        [MaxLength(50)]
        [Display(Name = "地址說明")]
        [JsonProperty("addressRemark")]
        public string AddressRemark { get; set; }

        [Display(Name = "活動開始時間")]
        [JsonProperty("activityStartDate")]
        public DateTime ActivityStartDate { get; set; }

        [Display(Name = "活動結束時間")]
        [JsonProperty("activityEndDate")]
        public DateTime ActivityEndDate { get; set; }

        [MaxLength(250)]
        [Display(Name = "活動簡介")]
        [JsonProperty("summary")]
        public string Summary { get; set; }

        [MaxLength(5000)]
        [Display(Name = "活動內容")]
        [JsonProperty("contentText")]
        public string ContentText { get; set; }

        [MaxLength(250)]
        [Display(Name = "活動注意事項")]
        [JsonProperty("pleaseNote")]
        public string PleaseNote { get; set; }

        [MaxLength(50)]
        [Display(Name = "主辦單位名稱")]
        [JsonProperty("organizerName")]
        public string OrganizerName { get; set; }

        [MaxLength(50)]
        [Display(Name = "主辦單位電話")]
        [JsonProperty("organizerPhone")]
        public string OrganizerPhone { get; set; }

        [MaxLength(50)]
        [Display(Name = "主辦單位信箱")]
        [JsonProperty("organizerMail")]
        public string OrganizerMail { get; set; }

        [MaxLength(200)]
        [Display(Name = "活動連結")]
        [JsonProperty("link")]
        public string Link { get; set; }

        [Display(Name = "活動費用")]
        [JsonProperty("price")]
        public int Price { get; set; }

        [Display(Name = "活動人數限制")]
        [JsonProperty("limitNumber")]
        public int LimitNumber { get; set; }

        [Display(Name = "開始報名時間")]
        [JsonProperty("startAcceptDate")]
        public DateTime StartAcceptDate { get; set; }

        [Display(Name = "結束報名時間")]
        [JsonProperty("endAcceptDate")]
        public DateTime EndAcceptDate { get; set; }

        [Display(Name = "活動發布狀態")]
        [JsonProperty("postState")]
        public bool PostState { get; set; }

        [Display(Name = "免費活動")]
        [JsonProperty("freeCost")]
        public bool FreeCost { get; set; }

        [Display(Name = "目前報名人數")]
        [JsonProperty("applicantNum")]
        public int ApplicantNumber { get; set; }

        [Display(Name = "報名額滿")]
        [JsonProperty("applicantNum")]
        public bool ApplicantFull { get; set; }

        [Display(Name = "活動檔案瀏覽次數")]
        [JsonProperty("views")]
        public int Views { get; set; }

        [Display(Name = "活動資訊收藏人數")]
        [JsonProperty("collectNumber")]
        public int CollectNumber { get; set; }

        [Display(Name = "建立時間")]
        [JsonProperty("creatDate")]
        public DateTime CreatDate { get; set; }

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
    }
}