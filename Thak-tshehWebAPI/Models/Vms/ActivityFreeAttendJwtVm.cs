using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 免費活動報名資料回應
    /// </summary>
    public class ActivityFreeAttendJwtVm
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// JwtToken
        /// </summary>
        public string JwtToken { get; set; }

        /// <summary>
        /// 免費活動報名資料
        /// </summary>
        public ActivityAttendData Data { get; set; }
    }

    /// <summary>
    /// 免費活動報名資料
    /// </summary>
    public class ActivityAttendData
    {
        /// <summary>
        /// 活動編號
        /// </summary>
        [Display(Name = "活動編號")]
        public int ActivityId { get; set; }

        /// <summary>
        /// 活動名稱
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "活動名稱")]
        public string ActivityName { get; set; }

        /// <summary>
        /// 活動型態分類
        /// </summary>
        [Display(Name = "活動型態分類")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ActivityType ActivityType { get; set; }

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
    }
}