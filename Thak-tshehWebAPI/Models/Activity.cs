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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Display(Name = "活動名稱")]
        public string Name { get; set; }

        [MaxLength(200)]
        [Display(Name = "圖檔名稱")]
        public string Image { get; set; }

        [Display(Name = "活動內容分類")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivityClass ActivityClass { get; set; }

        [Display(Name = "活動型態分類")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivityType ActivityType { get; set; }

        [Display(Name = "活動場域分類")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivityVenue ActivityVenue { get; set; }

        [Display(Name = "使用軟體分類")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Software Software { get; set; }

        [Display(Name = "地區分類")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Area Area { get; set; }

        [MaxLength(50)]
        [Display(Name = "地址")]
        public string Address { get; set; }

        [MaxLength(50)]
        [Display(Name = "地址說明")]
        public string AddressRemark { get; set; }

        [Display(Name = "活動開始時間")]
        public DateTime ActivityStartDate { get; set; }

        [Display(Name = "活動結束時間")]
        public DateTime ActivityEndDate { get; set; }

        [MaxLength(250)]
        [Display(Name = "活動簡介")]
        public string Summary { get; set; }

        [MaxLength(5000)]
        [Display(Name = "活動內容")]
        public string ContentText { get; set; }

        [MaxLength(250)]
        [Display(Name = "活動注意事項")]
        public string PleaseNote { get; set; }

        [MaxLength(50)]
        [Display(Name = "主辦單位名稱")]
        public string OrganizerName { get; set; }

        [MaxLength(50)]
        [Display(Name = "主辦單位電話")]
        public string OrganizerPhone { get; set; }

        [MaxLength(50)]
        [Display(Name = "主辦單位信箱")]
        public string OrganizerMail { get; set; }

        [MaxLength(200)]
        [Display(Name = "活動視訊連結")]
        public string Link { get; set; }

        [Display(Name = "活動費用")]
        public int Price { get; set; }

        [Display(Name = "活動人數限制")]
        public int LimitNumber { get; set; }

        [Display(Name = "開始報名時間")]
        public DateTime StartAcceptDate { get; set; }

        [Display(Name = "結束報名時間")]
        public DateTime EndAcceptDate { get; set; }

        [Display(Name = "活動發布狀態")]
        public bool PostState { get; set; }

        [Display(Name = "免費活動")]
        public bool FreeCost { get; set; }

        [Display(Name = "目前報名人數")]
        public int ApplicantNumber { get; set; }

        [Display(Name = "報名額滿")]
        public bool ApplicantFull { get; set; }

        [Display(Name = "活動檔案瀏覽次數")]
        public int Views { get; set; }

        [Display(Name = "活動資訊收藏人數")]
        public int CollectNumber { get; set; }

        [Display(Name = "活動評價平均星數")]
        public int EvaluateStars { get; set; }

        [Display(Name = "活動評價數量")]
        public int OpinionNumber { get; set; }

        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }


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


    }
}