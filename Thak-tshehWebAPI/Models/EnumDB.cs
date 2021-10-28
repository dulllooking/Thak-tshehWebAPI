using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thak_tshehWebAPI.Models
{
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

    public enum ActivityType
    {
        線上讀書會,
        實體讀書會,
        活動工作坊
    }

    public enum ActivityVenue
    {
        線上視訊軟體,
        實體活動場地
    }

    public enum Software
    {
        GoogleMeet,
        Zoom
    }

    public enum Area
    {
        基隆市,
        台北市,
        新北市,
        桃園市,
        新竹市,
        新竹縣,
        苗栗縣,
        台中市,
        彰化縣,
        南投縣,
        雲林縣,
        嘉義市,
        嘉義縣,
        台南市,
        高雄市,
        屏東縣,
        台東縣,
        花蓮縣,
        宜蘭縣,
        澎湖縣,
        金門縣,
        連江縣
    }
}