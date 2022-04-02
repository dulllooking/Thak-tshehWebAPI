using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 會員關注資料回應JWT
    /// </summary>
    public class UserFollowingDataJwtVm
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// JwtToken
        /// </summary>
        public string JwtToken { get; set; }

        /// <summary>
        /// 會員關注資料內容JWT
        /// </summary>
        public UserFollowingDataJWT Data { get; set; }
    }

    /// <summary>
    /// 會員關注資料內容JWT
    /// </summary>
    public class UserFollowingDataJWT
    {
        /// <summary>
        /// 關注中的會員資料頁數
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 會員關注資料JWT
        /// </summary>
        public FollowingUserDataJWT[] Following { get; set; }

    }

    /// <summary>
    /// 會員關注資料JWT
    /// </summary>
    public class FollowingUserDataJWT
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

        /// <summary>
        /// 是否關注中
        /// </summary>
        [Display(Name = "是否關注中")]
        public bool Following { get; set; }
    }
}