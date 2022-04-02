namespace Thak_tshehWebAPI.Models.Attributes
{
    /// <summary>
    /// 執行結果訊息JWT
    /// </summary>
    public class ApiMessageJwtResult
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
        /// Message
        /// </summary>
        public string Message { get; set; }
    }
}