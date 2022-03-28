namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 重設密碼資料
    /// </summary>
    public class PasswordResetVm
    {
        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 確認密碼
        /// </summary>
        public string CheckPassword { get; set; }
    }
}