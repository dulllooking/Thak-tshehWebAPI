using System;
using System.ComponentModel.DataAnnotations;

namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 活動評價資料回應
    /// </summary>
    public class ActivityOpinionListVm
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 活動評價資料
        /// </summary>
        public ActivityOpinionData Data { get; set; }
    }

    /// <summary>
    /// 活動評價資料
    /// </summary>
    public class ActivityOpinionData
    {
        /// <summary>
        /// 活動搜尋資料頁數
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 活動搜尋資料頁數
        /// </summary>
        public OpinionData[] Opinion { get; set; }
    }

    /// <summary>
    /// 活動評價資料
    /// </summary>
    public class OpinionData
    {
        /// <summary>
        /// 會員編號
        /// </summary>
        [Display(Name = "會員編號")]
        public int UserId { get; set; }

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
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }

        /// <summary>
        /// 評價星級
        /// </summary>
        [Display(Name = "評價星級")]
        public int Star { get; set; }

        /// <summary>
        /// 評價內容
        /// </summary>
        [MaxLength(250)]
        [Display(Name = "評價內容")]
        public string Opinion { get; set; }
    }
}