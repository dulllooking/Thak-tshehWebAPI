using System.ComponentModel.DataAnnotations;

namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 活動評價資料
    /// </summary>
    public class ActivityOpinionVm
    {
        /// <summary>
        /// 活動編號
        /// </summary>
        [Display(Name = "活動編號")]
        public int ActivityId { get; set; }

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