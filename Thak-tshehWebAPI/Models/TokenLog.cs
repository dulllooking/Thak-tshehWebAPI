using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Models
{
    public class TokenLog
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

        [MaxLength(50)]
        [Display(Name = "RefreshToken")]
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        [Display(Name = "建立時間")]
        [JsonProperty("creatDate")]
        public DateTime CreatDate { get; set; }

        [Display(Name = "過期時間")]
        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }
    }
}