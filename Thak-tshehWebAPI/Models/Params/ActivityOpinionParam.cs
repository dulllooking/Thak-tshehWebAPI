namespace Thak_tshehWebAPI.Models.Params
{
    /// <summary>
    /// 評價取得參數
    /// </summary>
    public class ActivityOpinionParam
    {
        /// <summary>
        /// 建構子
        /// </summary>
        public ActivityOpinionParam()
        {
            Split = 5;
            Page = 1;
        }

        /// <summary>
        /// 活動ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 每頁幾筆，預設9筆
        /// </summary>
        public int? Split { get; set; }

        /// <summary>
        /// 第幾頁，預設第1頁
        /// </summary>
        public int? Page { get; set; }
    }
}