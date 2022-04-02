namespace Thak_tshehWebAPI.Models.Vms
{
    /// <summary>
    /// 上傳圖片資料回應
    /// </summary>
    public class UploadProfileJwtVm
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
        /// 上傳圖片資料
        /// </summary>
        public UploadProfileData Data { get; set; }
    }

    /// <summary>
    /// 上傳圖片資料
    /// </summary>
    public class UploadProfileData
    {
        /// <summary>
        /// 圖片名稱
        /// </summary>
        public string FileName { get; set; }
    }
}