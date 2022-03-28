﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thak_tshehWebAPI.Models
{
    /// <summary>
    /// 活動評價紀錄
    /// </summary>
    public class ActivityOpinion
    {
        /// <summary>
        /// 編號
        /// </summary>
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 會員編號
        /// </summary>
        [Display(Name = "會員編號")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [Display(Name = "會員")]
        public virtual User User { get; set; }

        /// <summary>
        /// 活動編號
        /// </summary>
        [Display(Name = "活動編號")]
        public int ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        [Display(Name = "活動")]
        public virtual Activity Activity { get; set; }

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

        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }
    }
}