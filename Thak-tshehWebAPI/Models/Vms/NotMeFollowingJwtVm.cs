namespace Thak_tshehWebAPI.Models.Attributes
{
    /// <summary>
    /// 非本人且有無追蹤資料JWT
    /// </summary>
    public class NotMeFollowingJwtVm
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

        /// <summary>
        /// 有無追蹤
        /// </summary>
        public bool Following { get; set; }
    }
}