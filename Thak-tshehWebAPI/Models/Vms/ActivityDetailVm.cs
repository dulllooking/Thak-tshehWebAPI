using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 活動詳情資料回應
    /// </summary>
    public class ActivityDetailVm
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 活動詳情資料
        /// </summary>
        public ActivityDetailData Data { get; set; }
    }

    /// <summary>
    /// 活動詳情資料
    /// </summary>
    public class ActivityDetailData 
    {
        /// <summary>
        /// 活動資料全部內容
        /// </summary>
        public Activity ActivityData { get; set; }

        /// <summary>
        /// 活動舉辦者資料
        /// </summary>
        public OrganizerData OrganizerData { get; set; }
    }

    /// <summary>
    /// 活動舉辦者資料
    /// </summary>
    public class OrganizerData 
    {
        /// <summary>
        /// 會員編號
        /// </summary>
        [Key]
        [Display(Name = "會員編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 信箱帳號
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Display(Name = "信箱帳號")]
        public string Account { get; set; }

        /// <summary>
        /// 頭貼圖檔名稱
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "頭貼圖檔名稱")]
        public string Image { get; set; }

        /// <summary>
        /// 真實姓名
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "真實姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 會員暱稱
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "會員暱稱")]
        public string NickName { get; set; }

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
        /// 關於我
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "關於我")]
        public string AboutMe { get; set; }

        /// <summary>
        /// 個人檔案瀏覽次數
        /// </summary>
        [Display(Name = "個人檔案瀏覽次數")]
        public int Views { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }
    }
}