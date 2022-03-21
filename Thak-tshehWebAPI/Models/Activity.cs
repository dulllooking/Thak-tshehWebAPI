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
    /// 活動資料
    /// </summary>
    public class Activity
    {
        /// <summary>
        /// 活動編號
        /// </summary>
        [Key]
        [Display(Name = "活動編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 活動名稱
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "活動名稱")]
        public string Name { get; set; }

        /// <summary>
        /// 圖檔名稱
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "圖檔名稱")]
        public string Image { get; set; }

        /// <summary>
        /// 活動內容分類
        /// </summary>
        [Display(Name = "活動內容分類")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivityClass ActivityClass { get; set; }

        /// <summary>
        /// 活動型態分類
        /// </summary>
        [Display(Name = "活動型態分類")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivityType ActivityType { get; set; }

        /// <summary>
        /// 活動場域分類
        /// </summary>
        [Display(Name = "活動場域分類")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivityVenue ActivityVenue { get; set; }

        /// <summary>
        /// 使用軟體分類
        /// </summary>
        [Display(Name = "使用軟體分類")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Software Software { get; set; }

        /// <summary>
        /// 地區分類
        /// </summary>
        [Display(Name = "地區分類")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Area Area { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "地址")]
        public string Address { get; set; }

        /// <summary>
        /// 地址說明
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "地址說明")]
        public string AddressRemark { get; set; }

        /// <summary>
        /// 活動開始時間
        /// </summary>
        [Display(Name = "活動開始時間")]
        public DateTime ActivityStartDate { get; set; }

        /// <summary>
        /// 活動結束時間
        /// </summary>
        [Display(Name = "活動結束時間")]
        public DateTime ActivityEndDate { get; set; }

        /// <summary>
        /// 活動簡介
        /// </summary>
        [MaxLength(250)]
        [Display(Name = "活動簡介")]
        public string Summary { get; set; }

        /// <summary>
        /// 活動內容
        /// </summary>
        [MaxLength(5000)]
        [Display(Name = "活動內容")]
        public string ContentText { get; set; }

        /// <summary>
        /// 活動注意事項
        /// </summary>
        [MaxLength(250)]
        [Display(Name = "活動注意事項")]
        public string PleaseNote { get; set; }

        /// <summary>
        /// 主辦單位名稱
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "主辦單位名稱")]
        public string OrganizerName { get; set; }

        /// <summary>
        /// 主辦單位電話
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "主辦單位電話")]
        public string OrganizerPhone { get; set; }

        /// <summary>
        /// 主辦單位信箱
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "主辦單位信箱")]
        public string OrganizerMail { get; set; }

        /// <summary>
        /// 活動視訊連結
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "活動視訊連結")]
        public string Link { get; set; }

        /// <summary>
        /// 活動費用
        /// </summary>
        [Display(Name = "活動費用")]
        public int Price { get; set; }

        /// <summary>
        /// 活動人數限制
        /// </summary>
        [Display(Name = "活動人數限制")]
        public int LimitNumber { get; set; }

        /// <summary>
        /// 開始報名時間
        /// </summary>
        [Display(Name = "開始報名時間")]
        public DateTime StartAcceptDate { get; set; }

        /// <summary>
        /// 結束報名時間
        /// </summary>
        [Display(Name = "結束報名時間")]
        public DateTime EndAcceptDate { get; set; }

        /// <summary>
        /// 活動發布狀態
        /// </summary>
        [Display(Name = "活動發布狀態")]
        public bool PostState { get; set; }

        /// <summary>
        /// 免費活動
        /// </summary>
        [Display(Name = "免費活動")]
        public bool FreeCost { get; set; }

        /// <summary>
        /// 目前報名人數
        /// </summary>
        [Display(Name = "目前報名人數")]
        public int ApplicantNumber { get; set; }

        /// <summary>
        /// 報名額滿
        /// </summary>
        [Display(Name = "報名額滿")]
        public bool ApplicantFull { get; set; }

        /// <summary>
        /// 活動檔案瀏覽次數
        /// </summary>
        [Display(Name = "活動檔案瀏覽次數")]
        public int Views { get; set; }

        /// <summary>
        /// 活動資訊收藏人數
        /// </summary>
        [Display(Name = "活動資訊收藏人數")]
        [JsonProperty("collectNumber")]
        public int CollectNumber { get; set; }

        /// <summary>
        /// 活動評價平均星數
        /// </summary>
        [Display(Name = "活動評價平均星數")]
        public int EvaluateStars { get; set; }

        /// <summary>
        /// 活動評價數量
        /// </summary>
        [Display(Name = "活動評價數量")]
        public int OpinionNumber { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<ActivityLog> ActivityLog { get; set; }

        [JsonIgnore]
        public virtual ICollection<ActivityCollect> ActivityCollect { get; set; }

        [JsonIgnore]
        public virtual ICollection<ActivityOpinion> ActivityOpinion { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrganizerLog> OrganizerLog { get; set; }
    }
}