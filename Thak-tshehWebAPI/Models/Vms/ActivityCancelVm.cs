using System.ComponentModel.DataAnnotations;

namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    ///  活動取消資料
    /// </summary>
    public class ActivityCancelVm
    {
        /// <summary>
        /// 活動編號
        /// </summary>
        [Display(Name = "活動編號")]
        public int ActivityId { get; set; }
    }
}