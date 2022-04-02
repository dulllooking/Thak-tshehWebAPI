using System.ComponentModel.DataAnnotations;

namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 活動報名請求資料
    /// </summary>
    public class ActivityRequestVm
    {
        /// <summary>
        /// 活動ID
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// 活動價格
        /// </summary>
        public int ActivityPrice { get; set; }

        /// <summary>
        /// 使用者ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 使用者手機號碼
        /// </summary>
        [Required]
        public string UserMobilePhone { get; set; }

        /// <summary>
        /// 使用者帳號
        /// </summary>
        [Required]
        public string UserAccount { get; set; }
    }
}