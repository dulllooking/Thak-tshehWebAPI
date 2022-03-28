namespace Thak_tshehWebAPI.Models.Attributes
{
    /// <summary>
    /// 執行結果訊息
    /// </summary>
    public class ApiMessageResult
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
    }
}