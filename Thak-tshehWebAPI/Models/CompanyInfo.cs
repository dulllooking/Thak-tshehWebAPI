using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Models
{
    public class CompanyInfo
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Display(Name = "公司名稱")]
        public string Name { get; set; }

        [MaxLength(50)]
        [Display(Name = "公司電話")]
        public string Phone { get; set; }

        [MaxLength(50)]
        [Display(Name = "公司信箱")]
        public string Email { get; set; }

        [MaxLength(200)]
        [Display(Name = "Facebook連結")]
        public string FacebookLink { get; set; }

        [MaxLength(200)]
        [Display(Name = "Instagram連結")]
        public string InstagramLink { get; set; }

        [MaxLength(200)]
        [Display(Name = "Line連結")]
        public string LineLink { get; set; }

        [Display(Name = "關於我們")]
        public string AboutUs { get; set; }

        [Display(Name = "常見問題")]
        public string Questions { get; set; }

        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }
    }
}