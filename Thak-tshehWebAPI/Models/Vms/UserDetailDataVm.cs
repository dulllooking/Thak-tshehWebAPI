using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 使用者個人細節資料回應
    /// </summary>
    public class UserDetailDataVm
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 使用者個人細節資料
        /// </summary>
        public UserDetailData Data { get; set; }
    }

    /// <summary>
    /// 使用者個人細節資料
    /// </summary>
    public class UserDetailData
    {
        /// <summary>
        /// 會員編號
        /// </summary>
        [Display(Name = "會員編號")]
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
        /// 會員追蹤者人數
        /// </summary>
        [Display(Name = "會員追蹤者人數")]
        public int FollowersNumber { get; set; }

        /// <summary>
        /// 會員關注中人數
        /// </summary>
        [Display(Name = "會員關注中人數")]
        public int FollowingNumber { get; set; }

        /// <summary>
        /// 國家
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "國家")]
        public string Country { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "城市")]
        public string City { get; set; }

        /// <summary>
        /// 地區
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "地區")]
        public string Area { get; set; }

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
        /// 我的專長
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "我的專長")]
        public string MySkill { get; set; }

        /// <summary>
        /// 我的興趣
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "我的興趣")]
        public string MyInterest { get; set; }

        /// <summary>
        /// 顯示即將到臨的活動
        /// </summary>
        [Display(Name = "顯示即將到臨的活動")]
        public bool ShowComingActivity { get; set; }

        /// <summary>
        /// 顯示收藏的活動
        /// </summary>
        [Display(Name = "顯示收藏的活動")]
        public bool ShowCollectActivity { get; set; }

        /// <summary>
        /// 顯示已完成的活動
        /// </summary>
        [Display(Name = "顯示已完成的活動")]
        public bool ShowFinishActivity { get; set; }

        /// <summary>
        /// 顯示關注中的用戶
        /// </summary>
        [Display(Name = "顯示關注中的用戶")]
        public bool ShowFollowing { get; set; }

        /// <summary>
        /// 顯示追蹤者
        /// </summary>
        [Display(Name = "顯示追蹤者")]
        public bool ShowFollowers { get; set; }

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