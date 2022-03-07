#pragma warning disable CS1591 // 遺漏公用可見類型或成員的 XML 註解

namespace Thak_tshehWebAPI.Models
{
    /// <summary>
    /// 活動類別
    /// </summary>
    public enum ActivityClass
    {
        商業理財,
        文學小說,
        人文史地,
        醫療保健,
        生活風格,
        自然科學,
        電腦資訊
    }

    /// <summary>
    /// 活動型式
    /// </summary>
    public enum ActivityType
    {
        線上讀書會,
        實體讀書會,
        活動工作坊
    }

    /// <summary>
    /// 活動場域
    /// </summary>
    public enum ActivityVenue
    {
        線上視訊軟體,
        實體活動場地
    }

    /// <summary>
    /// 軟體名稱
    /// </summary>
    public enum Software
    {
        GoogleMeet,
        Zoom
    }

    /// <summary>
    /// 縣市
    /// </summary>
    public enum Area
    {
        // 北部 0~6
        台北市,
        新北市,
        基隆市,
        桃園市,
        新竹市,
        新竹縣,
        宜蘭縣,
        // 中部 7~11
        苗栗縣,
        台中市,
        彰化縣,
        南投縣,
        雲林縣,
        // 南部 12~16
        嘉義市,
        嘉義縣,
        台南市,
        高雄市,
        屏東縣,
        // 東部 17~18
        台東縣,
        花蓮縣,
        澎湖縣,
        金門縣,
        連江縣
    }

    /// <summary>
    /// 性別
    /// </summary>
    public enum Sex
    {
        生理男,
        生理女,
        不公開
    }
}