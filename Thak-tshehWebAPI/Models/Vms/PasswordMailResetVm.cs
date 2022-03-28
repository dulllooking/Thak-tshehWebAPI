namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 信箱連入重設密碼資料
    /// </summary>
    public class PasswordMailResetVm
    {
        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 確認密碼
        /// </summary>
        public string CheckPassword { get; set; }

        /// <summary>
        /// 信箱驗證 GUID
        /// </summary>
        public string Guid { get; set; }
    }
}