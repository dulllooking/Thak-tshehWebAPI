namespace Thak_tshehWebAPI.Models.Params
{
    /// <summary>
    /// 官方推薦參數
    /// </summary>
    public class RecommendActivityParam
    {
        /// <summary>
        /// 建構子
        /// </summary>
        public RecommendActivityParam()
        {
            Type = 0;
            Split = 3;
            Page = 1;
        }

        /// <summary>
        /// (-1所有主題)，0線上讀書會，1實體讀書會，2活動工作坊
        /// </summary>
        public int? Type { get; set; }

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