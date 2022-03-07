using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Models
{
    public class ActivityCollect
    {
        [Key]
        [Display(Name = "編號")]
        [JsonProperty("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [Display(Name = "會員編號")]
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [Display(Name = "會員")]
        public virtual User User { get; set; }

        //ForeignKey
        [Display(Name = "活動編號")]
        [JsonProperty("activityId")]
        public int ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        [Display(Name = "活動")]
        public virtual Activity Activity { get; set; }

        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }
    }
}