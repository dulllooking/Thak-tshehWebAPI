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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //ForeignKey
        [Display(Name = "會員編號")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [Display(Name = "會員")]
        public virtual User User { get; set; }

        [MaxLength(50)]
        [Display(Name = "RefreshToken")]
        public string RefreshToken { get; set; }

        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }

        [Display(Name = "過期時間")]
        public DateTime? EndDate { get; set; }
    }
}