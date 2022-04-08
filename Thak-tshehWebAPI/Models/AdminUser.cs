using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Models
{
    /// <summary>
    /// 管理員資料
    /// </summary>
    public class AdminUser
    {
        /// <summary>
        /// 會員編號
        /// </summary>
        [Key]
        [Display(Name = "管理員編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Required]
        [MaxLength(100)]
        [Display(Name = "密碼")]
        public string HashPassword { get; set; }

        /// <summary>
        /// 雜湊鹽
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "雜湊鹽")]
        public string Salt { get; set; }
    }
}