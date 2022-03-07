using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Models
{
    /// <summary>
    /// 使用者追蹤資料
    /// </summary>
    public class UserFollowers
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
        /// 關注會員編號
        /// </summary>
        [Display(Name = "關注會員編號")]
        public int FollowingUserId { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }
    }
}