using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 會員追蹤者資料回應
    /// </summary>
    public class UserFollowersDataVm
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 會員追蹤者資料內容
        /// </summary>
        public UserFollowersData Data { get; set; }
    }

    /// <summary>
    /// 會員追蹤者資料內容
    /// </summary>
    public class UserFollowersData
    {
        /// <summary>
        /// 追蹤者會員資料頁數
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 會員追蹤者資料
        /// </summary>
        public FollowersUserData[] Followers { get; set; }

    }

    /// <summary>
    /// 會員追蹤者資料
    /// </summary>
    public class FollowersUserData
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
        /// 關於我
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "關於我")]
        public string AboutMe { get; set; }

        /// <summary>
        /// 關注時間
        /// </summary>
        [Display(Name = "關注時間")]
        public DateTime? FollowingData { get; set; }

        /// <summary>
        /// 追蹤者會員編號
        /// </summary>
        [Display(Name = "追蹤者會員編號")]
        public int UserId { get; set; }

        /// <summary>
        /// 關注中會員編號
        /// </summary>
        [Display(Name = "關注中會員編號")]
        public int FollowingUserId { get; set; }
    }
}