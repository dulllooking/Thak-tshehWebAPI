﻿using Newtonsoft.Json;
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
        [Display(Name = "編號")]
        [JsonProperty("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [Display(Name = "會員編號")]
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        //ForeignKey
        [Display(Name = "活動編號")]
        [JsonProperty("activityId")]
        public int ActivityId { get; set; }
        [ForeignKey("ActivityId")]
        public virtual Activity Activity { get; set; }

        [Display(Name = "報名狀態")]
        [JsonProperty("orderState")]
        public bool OrderState { get; set; }

        [Display(Name = "建立時間")]
        [JsonProperty("creatDate")]
        public DateTime CreatDate { get; set; }
    }
}