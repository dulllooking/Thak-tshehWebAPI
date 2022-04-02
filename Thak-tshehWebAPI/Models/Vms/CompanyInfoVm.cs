using System.ComponentModel.DataAnnotations;

namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 公司相關資料回應
    /// </summary>
    public class CompanyInfoVm
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 公司資料
        /// </summary>
        public CompanyInfoData Data { get; set; }

    }

    /// <summary>
    /// 公司資料
    /// </summary>
    public class CompanyInfoData 
    {
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
    }
}