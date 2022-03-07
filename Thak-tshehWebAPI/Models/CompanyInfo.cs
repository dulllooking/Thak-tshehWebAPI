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
    /// 公司資料
    /// </summary>
    public class CompanyInfo
    {
        /// <summary>
        /// 編號
        /// </summary>
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "公司名稱")]
        public string Name { get; set; }

        /// <summary>
        /// 公司電話
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "公司電話")]
        public string Phone { get; set; }

        /// <summary>
        /// 公司信箱
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "公司信箱")]
        public string Email { get; set; }

        /// <summary>
        /// Facebook連結
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "Facebook連結")]
        public string FacebookLink { get; set; }

        /// <summary>
        /// Instagram連結
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "Instagram連結")]
        public string InstagramLink { get; set; }

        /// <summary>
        /// Line連結
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "Line連結")]
        public string LineLink { get; set; }

        /// <summary>
        /// 關於我們
        /// </summary>
        [Display(Name = "關於我們")]
        public string AboutUs { get; set; }

        /// <summary>
        /// 常見問題
        /// </summary>
        [Display(Name = "常見問題")]
        public string Questions { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }
    }
}