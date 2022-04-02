using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 會員完成活動資料回應
    /// </summary>
    public class UserActivityAttendDoneDataVm
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 會員完成活動資料內容
        /// </summary>
        public UserActivityAttendDoneData Data { get; set; }
    }

    /// <summary>
    /// 會員完成活動資料內容
    /// </summary>
    public class UserActivityAttendDoneData
    {
        /// <summary>
        /// 會員參加活動資料頁數
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 會員完成活動資料
        /// </summary>
        public ActivityAttendDoneDetail[] MyActivity { get; set; }
    }

    /// <summary>
    /// 會員完成活動資料
    /// </summary>
    public class ActivityAttendDoneDetail
    {
        /// <summary>
        /// 活動編號
        /// </summary>
        [Display(Name = "活動編號")]
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
        /// 主辦單位名稱
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "主辦單位名稱")]
        public string OrganizerName { get; set; }

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
        /// 目前報名人數
        /// </summary>
        [Display(Name = "目前報名人數")]
        public int ApplicantNumber { get; set; }

        /// <summary>
        /// 活動檔案瀏覽次數
        /// </summary>
        [Display(Name = "活動檔案瀏覽次數")]
        public int Views { get; set; }

        /// <summary>
        /// 活動資訊收藏人數
        /// </summary>
        [Display(Name = "活動資訊收藏人數")]
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
        /// 活動評價完成
        /// </summary>
        [Display(Name = "活動評價完成")]
        public bool OpinionsDone { get; set; }
    }
}