using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thak_tshehWebAPI.Models
{
    /// <summary>
    /// 活動訂單紀錄
    /// </summary>
    public class ActivityLog
    {
        /// <summary>
        /// 訂單編號
        /// </summary>
        [Key]
        [Display(Name = "訂單編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 活動編號
        /// </summary>
        [Display(Name = "活動編號")]
        public int ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        [Display(Name = "活動")]
        public virtual Activity Activity { get; set; }

        /// <summary>
        /// 活動費用
        /// </summary>
        [Display(Name = "活動費用")]
        public int? ActivityPrice { get; set; }

        /// <summary>
        /// 會員報名狀態
        /// </summary>
        [Display(Name = "會員報名狀態")]
        public bool? OrderState { get; set; }

        /// <summary>
        /// 會員編號
        /// </summary>
        [Display(Name = "會員編號")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [Display(Name = "會員")]
        public virtual User User { get; set; }

        /// <summary>
        /// 會員真實姓名
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "會員真實姓名")]
        public string UserName { get; set; }

        /// <summary>
        /// 會員手機號碼
        /// </summary>
        [MaxLength(20)]
        [Display(Name = "會員手機號碼")]
        public string UserMobilePhone { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }
    }
}