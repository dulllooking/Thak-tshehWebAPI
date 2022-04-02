using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;

namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 活動卡片資料回應JWT
    /// </summary>
    public class ActivityViewsJwtVm
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
        /// 活動卡片資料
        /// </summary>
        public ActivityViewsDataJWT[] Data { get; set; }
    }

    /// <summary>
    /// 活動卡片資料
    /// </summary>
    public class ActivityViewsDataJWT
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
        [JsonConverter(typeof(StringEnumConverter))] // 列舉轉字串
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
        /// 活動人數限制
        /// </summary>
        [Display(Name = "活動人數限制")]
        public int LimitNumber { get; set; }

        /// <summary>
        /// 活動檔案瀏覽次數
        /// </summary>
        [Display(Name = "活動檔案瀏覽次數")]
        public int Views { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }

        /// <summary>
        /// 使用者是否收藏
        /// </summary>
        [Display(Name = "使用者是否收藏")]
        public bool UserCollected { get; set; }
    }
}