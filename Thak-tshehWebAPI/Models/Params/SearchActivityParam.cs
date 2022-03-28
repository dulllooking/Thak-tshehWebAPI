namespace Thak_tshehWebAPI.Models.Params
{
    /// <summary>
    /// 活動搜尋參數
    /// </summary>
    public class SearchActivityParam
    {
        /// <summary>
        /// 建構子
        /// </summary>
        public SearchActivityParam()
        {
            Split = 9;
            Page = 1;
            Type = -1;
            Classify = -1;
            Area = -1;
            Sorting = 0;
            Query = "%E3%80%8A";
        }

        /// <summary>
        /// 每頁幾筆，預設9筆
        /// </summary>
        public int? Split { get; set; }

        /// <summary>
        /// 第幾頁，預設第1頁
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// (-1所有主題)，0線上讀書會，1實體讀書會，2活動工作坊
        /// </summary>
        public int? Type { get; set; }

        /// <summary>
        /// (-1所有分類)，0商業理財，1文學小說，2人文史地，3醫療保健，4生活風格，5自然科學，6電腦資訊
        /// </summary>
        public int? Classify { get; set; }

        /// <summary>
        /// (-1不分區)，0北部(台北市.新北市.基隆市.桃園市.新竹市.新竹縣.宜蘭縣)，1中部(苗栗縣.台中市.彰化縣.南投縣.雲林縣)，2南部(嘉義市.嘉義縣.台南市.高雄市.屏東縣)，3東部(台東縣.花蓮縣)，4外島(澎湖縣.金門縣.連江縣)
        /// </summary>
        public int? Area { get; set; }

        /// <summary>
        /// 0最新發佈(預設-以活動開始時間排序，由近到遠)，1最熱門活動(以瀏覽人數排序，由多到少)，2日期由新到舊(以開放報名時間排序，由近到遠)
        /// </summary>
        public int? Sorting { get; set; }

        /// <summary>
        /// 需使用encodeURI()將搜尋字串轉碼，可接受用1個空格區隔字串，做為同時搜尋2個關鍵字，最多只能2個關鍵字，超過的會無視，預設為標題全形括號("《")encodeURI後的"%E3%80%8A"
        /// </summary>
        public string Query { get; set; }
    }
}