namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 訂單使用者資料
    /// </summary>
    public class UserRealDataJwtVm
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
        /// 使用者資料
        /// </summary>
        public UserRealData Data { get; set; }
    }

    /// <summary>
    /// 使用者資料
    /// </summary>
    public class UserRealData
    {
        /// <summary>
        /// 使用者ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 使用者手機號碼
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string Account { get; set; }
    }
}