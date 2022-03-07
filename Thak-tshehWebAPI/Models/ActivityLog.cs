using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Models
{
    public class ActivityLog
    {
        [Key]
        [Display(Name = "訂單編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [Display(Name = "活動編號")]
        public int ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        [Display(Name = "活動")]
        public virtual Activity Activity { get; set; }

        [Display(Name = "活動費用")]
        public int? ActivityPrice { get; set; }

        [Display(Name = "會員報名狀態")]
        public bool? OrderState { get; set; }

        //ForeignKey
        [Display(Name = "會員編號")]
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [Display(Name = "會員")]
        public virtual User User { get; set; }

        [MaxLength(100)]
        [Display(Name = "會員真實姓名")]
        public string UserName { get; set; }

        [MaxLength(20)]
        [Display(Name = "會員手機號碼")]
        public string UserMobilePhone { get; set; }

        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }
    }
}