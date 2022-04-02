namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 付費活動報名付款前資料回應
    /// </summary>
    public class PaymentSubmitJwtVm
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
        /// 付費活動報名付款前資料
        /// </summary>
        public PaymentSubmitData PaymentData { get; set; }
    }

    /// <summary>
    /// 付費活動報名付款前資料
    /// </summary>
    public class PaymentSubmitData
    {
        /// <summary>
        /// MerchantID
        /// </summary>
        public string MerchantID { get; set; }

        /// <summary>
        /// TradeInfo
        /// </summary>
        public string TradeInfo { get; set; }

        /// <summary>
        /// TradeSha
        /// </summary>
        public string TradeSha { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; }
    }
}