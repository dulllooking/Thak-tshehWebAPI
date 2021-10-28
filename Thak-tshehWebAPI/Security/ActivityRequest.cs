using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Security
{
    public class ActivityRequest
    {
        public int ActivityId { get; set; }

        public int ActivityPrice { get; set; }

        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserMobilePhone { get; set; }

        [Required]
        public string UserAccount { get; set; }
    }
}