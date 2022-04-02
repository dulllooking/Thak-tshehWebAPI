namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 個人資料回應
    /// </summary>
    public class ProfileJwtVm
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
        /// 個人資料
        /// </summary>
        public ProfileData Data { get; set; }
    }

    /// <summary>
    /// 個人資料
    /// </summary>
    public class ProfileData
    {
        /// <summary>
        /// 使用者ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 使用者暱稱
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 使用者圖片
        /// </summary>
        public string Image { get; set; }
    }
}