using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Thak_tshehWebAPI.Models;
using Thak_tshehWebAPI.Security;

namespace Thak_tshehWebAPI.Controllers
{
    // 啟用跨網域存取
    [EnableCors("*", "*", "*")]
    public class ActivitiesController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // 1-1 熱門讀書會資料
        // GET: api/activities/top-views
        [HttpGet]
        [Route("api/activities/top-views")]
        public IHttpActionResult GetActivityTop()
        {
            var infoType0 = db.Activity.Where(x => ((int)x.ActivityType) == 0).OrderByDescending(x => x.Views).Select(x => new
            {
                x.Id,
                x.Name,
                x.Image,
                x.ActivityStartDate,
                x.ActivityEndDate,
                x.LimitNumber,
                x.Summary,
                x.OrganizerName,
                ActivityType = x.ActivityType.ToString(),
                x.Views,
                x.CreatDate
            }).FirstOrDefault();

            var infoType1 = db.Activity.Where(x => ((int)x.ActivityType) == 1).OrderByDescending(x => x.Views).Select(x => new
            {
                x.Id,
                x.Name,
                x.Image,
                x.ActivityStartDate,
                x.ActivityEndDate,
                x.LimitNumber,
                x.Summary,
                x.OrganizerName,
                ActivityType = x.ActivityType.ToString(),
                x.Views,
                x.CreatDate
            }).FirstOrDefault();

            var infoType2 = db.Activity.Where(x => ((int)x.ActivityType) == 2).OrderByDescending(x => x.Views).Select(x => new
            {
                x.Id,
                x.Name,
                x.Image,
                x.ActivityStartDate,
                x.ActivityEndDate,
                x.LimitNumber,
                x.Summary,
                x.OrganizerName,
                ActivityType = x.ActivityType.ToString(),
                x.Views,
                x.CreatDate
            }).FirstOrDefault();

            // 轉成物件陣列
            Object[] dataArray = new Object[3] { infoType0, infoType1, infoType2 };

            return Json(new { Status = true, Data = dataArray });
        }

        // 1-2 1-3 1-4 滾動區資料
        // GET: api/activities/new9/type/類型(0~2)
        [HttpGet]
        [Route("api/activities/new9/type/{type}")]
        public IHttpActionResult GetNew9Activity(int type)
        {
            var info = db.Activity.Where(x => ((int)x.ActivityType) == type).OrderByDescending(x => x.CreatDate).Select(x => new
            {
                x.Id,
                x.Name,
                x.Image,
                x.ActivityStartDate,
                x.ActivityEndDate,
                x.LimitNumber,
                x.Summary,
                x.OrganizerName,
                ActivityType = x.ActivityType.ToString(),
                x.Views,
                x.CreatDate
            }).Take(9);

            return Json(new { Status = true, Data = info });
        }

        // 1-7 Navbar 搜尋功能
        // GET: api/activity/search/每頁幾筆/第幾頁/類型/分類/地區/排序/關鍵字 (用空格區隔最多可查兩個關鍵字)
        [HttpGet]
        [Route("api/activity/search/{split:int?}/{page:int?}/{type:int?}/{classify:int?}/{area:int?}/{sorting:int?}/{query?}")]
        public IHttpActionResult GetSearchActivity(int split = 9, int page = 1, int type = -1, int classify = -1, int area = -1, int sorting = 0, string query = "%E3%80%8A")
        {
            // 前端會使用encodeURI(關鍵字)，因此需要解碼
            string decodeKeyWord = HttpUtility.UrlDecode(query).Trim();
            // 切割取出兩組關鍵字
            string[] keyWordArr = decodeKeyWord.Split(' ');
            string keyWord1 = keyWordArr[0].ToString();
            // 只有一個關鍵字時，就查詢奇怪的東西
            string keyWord2 = keyWordArr.Length>1 ? keyWordArr[1].ToString() : "~!@#$%^&*()_+";

            // var info = db.Activity; // 沒加查詢語句時型別是 DBset 後面無法再取出查詢
            // 多個獨立條件需分開用.Where()，不然結果不是預期的東西
            var info = db.Activity.Where(x => type < 0 || (int)x.ActivityType == type)
                .Where(x => classify < 0 || (int)x.ActivityClass == classify)
                .Where(x => (x.Name.Contains(keyWord1) || x.OrganizerName.Contains(keyWord1) || x.Summary.Contains(keyWord1) || x.ContentText.Contains(keyWord1)) || (x.Name.Contains(keyWord2) || x.OrganizerName.Contains(keyWord2) || x.Summary.Contains(keyWord2) || x.ContentText.Contains(keyWord2)))
                .Where(x => area==0? (int)x.Area <= 6 : area==1? ((int)x.Area > 6 || (int)x.Area <= 11) : area==2? ((int)x.Area > 11 || (int)x.Area <= 16) : area==3? ((int)x.Area > 16 || (int)x.Area <= 18) : area==4 ? (int)x.Area > 18 : (int)x.Area >= 0)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Image,
                    x.ActivityStartDate,
                    x.ActivityEndDate,
                    x.LimitNumber,
                    x.Summary,
                    x.OrganizerName,
                    ActivityType = x.ActivityType.ToString(),
                    ActivityClass = x.ActivityClass.ToString(),
                    ActivityArea = x.Area.ToString(),
                    x.Views,
                    x.ApplicantNumber,
                    x.CollectNumber,
                    x.EvaluateStars,
                    x.OpinionNumber,
                    x.Price,
                    x.StartAcceptDate,
                    x.EndAcceptDate
                });

            // 排序功能
            switch (sorting) {
                case 0: // 最新發佈 (預設) => 以活動開始時間排序，由近到遠
                    info = info.OrderByDescending(x => x.StartAcceptDate);
                    break;
                case 1: // 最熱門活動 => 以瀏覽人數排序，由多到少
                    info = info.OrderByDescending(x => x.Views);
                    break;
                case 2: // 日期由新到舊 => 以開放報名時間排序，由近到遠
                    info = info.OrderByDescending(x => x.ActivityStartDate);
                    break;
                default:
                    break;
            }

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)info.Count() / split);

            // .Skip() 前一定要接 .OrderBy()
            // 刷新 JwtToken 並送出活動資料
            return Ok(new
            {
                Status = true,
                Data = new
                {
                    TotalPage = totalPageNumber,
                    Activity = info.Skip(split * (page - 1)).Take(split)
                }
            });
        }

        // 4-1 即將截止報名資料
        // GET: api/activity/final/type/類型/每頁幾筆/第幾頁
        [HttpGet]
        [Route("api/activity/final/type/{type:int?}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetFinal4HourActivity(int type = 0, int split = 3, int page = 1)
        {
            var info = db.Activity.Where(x => (int)x.ActivityType == type && x.EndAcceptDate > DateTime.Now && !x.ApplicantFull).Select(x => new
            {
                x.Id,
                x.Name,
                x.Image,
                x.ActivityStartDate,
                x.ActivityEndDate,
                x.LimitNumber,
                x.Summary,
                x.OrganizerName,
                ActivityType = x.ActivityType.ToString(),
                ActivityClass = x.ActivityClass.ToString(),
                ActivityArea = x.Area.ToString(),
                x.Views,
                x.ApplicantNumber,
                x.CollectNumber,
                x.EvaluateStars,
                x.OpinionNumber,
                x.Price,
                x.StartAcceptDate,
                x.EndAcceptDate
            }).OrderBy(x => x.EndAcceptDate);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)info.Count() / split);

            // 刷新 JwtToken 並送出活動資料
            return Ok(new
            {
                Status = true,
                Data = new
                {
                    TotalPage = totalPageNumber,
                    Activity = info.Skip(split * (page - 1)).Take(split)
                }
            });
        }

        // 4-2 最多人報名資料
        // GET: api/activity/hot/type/類型/每頁幾筆/第幾頁
        [HttpGet]
        [Route("api/activity/hot/type/{type:int?}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetHotActivity(int type = 0, int split = 3, int page = 1)
        {
            var info = db.Activity.Where(x => (int)x.ActivityType == type && x.EndAcceptDate > DateTime.Now && !x.ApplicantFull).Select(x => new
            {
                x.Id,
                x.Name,
                x.Image,
                x.ActivityStartDate,
                x.ActivityEndDate,
                x.LimitNumber,
                x.Summary,
                x.OrganizerName,
                ActivityType = x.ActivityType.ToString(),
                ActivityClass = x.ActivityClass.ToString(),
                ActivityArea = x.Area.ToString(),
                x.Views,
                x.ApplicantNumber,
                x.CollectNumber,
                x.EvaluateStars,
                x.OpinionNumber,
                x.Price,
                x.StartAcceptDate,
                x.EndAcceptDate
            }).OrderByDescending(x => x.ApplicantNumber);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)info.Count() / split);

            // 刷新 JwtToken 並送出活動資料
            return Ok(new
            {
                Status = true,
                Data = new
                {
                    TotalPage = totalPageNumber,
                    Activity = info.Skip(split * (page - 1)).Take(split)
                }
            });
        }

        // 4-3 新推出資料
        // GET: api/activity/new/type/類型/每頁幾筆/第幾頁
        [HttpGet]
        [Route("api/activity/new/type/{type:int?}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetNewActivity(int type = 0, int split = 3, int page = 1)
        {
            var info = db.Activity.Where(x => (int)x.ActivityType == type && x.EndAcceptDate > DateTime.Now && !x.ApplicantFull).Select(x => new
            {
                x.Id,
                x.Name,
                x.Image,
                x.ActivityStartDate,
                x.ActivityEndDate,
                x.LimitNumber,
                x.Summary,
                x.OrganizerName,
                ActivityType = x.ActivityType.ToString(),
                ActivityClass = x.ActivityClass.ToString(),
                ActivityArea = x.Area.ToString(),
                x.Views,
                x.ApplicantNumber,
                x.CollectNumber,
                x.EvaluateStars,
                x.OpinionNumber,
                x.Price,
                x.StartAcceptDate,
                x.EndAcceptDate
            }).OrderByDescending(x => x.StartAcceptDate);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)info.Count() / split);

            // 刷新 JwtToken 並送出活動資料
            return Ok(new
            {
                Status = true,
                Data = new
                {
                    TotalPage = totalPageNumber,
                    Activity = info.Skip(split * (page - 1)).Take(split)
                }
            });
        }

        // 5-1 活動+舉辦者資料
        // GET: api/activity/id/活動ID
        [HttpGet]
        [Route("api/activity/id/{id}")]
        public IHttpActionResult GetActivityDataById(int id)
        {
            // 查詢活動舉辦記錄
            var organizerData = db.OrganizerLog.Where(x => x.ActivityId == id).Select(x => new
            {
                x.User.Id,
                x.User.Account,
                x.User.Image,
                x.User.Name,
                x.User.NickName,
                x.User.AboutMe,
                x.User.Views,
                x.User.FacebookLink,
                x.User.InstagramLink,
                x.User.CreatDate
            }).FirstOrDefault();

            // 查詢指定活動
            var activityData = db.Activity.FirstOrDefault(x => x.Id == id);

            return Json(new { Status = true, Data = new { ActivityData = activityData, OrganizerData = organizerData } });
        }

        // 5-2 增加活動瀏覽次數
        // PUT: api/organizer/activity/views
        [HttpPut]
        [Route("api/organizer/activity/views")]
        public IHttpActionResult AddActivityViews([FromBody] ActivityRequest activityData)
        {
            // 查詢指定活動
            var activityQuery = db.Activity.FirstOrDefault(x => x.Id == activityData.ActivityId);

            // 增加瀏覽次數
            activityQuery.Views += 1;
            db.SaveChanges();

            return Ok(new { Status = true, Message = "活動瀏覽次數增加成功" });
        }

        // 5-3 活動評價資料+分頁
        // GET: api/activity/opinion/活動ID/每頁幾筆/第幾頁
        [HttpGet]
        [Route("api/activity/opinion/{id}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetActivityOpinionList(int id, int split = 5, int page = 1)
        {
            // 查詢活動參加記錄
            var opinionList = db.ActivityOpinion.Where(x => x.ActivityId == id).Select(x => new
            {
                x.UserId,
                x.User.Image,
                x.User.Name,
                x.CreatDate,
                x.Star,
                x.Opinion
            }).OrderByDescending(x => x.CreatDate);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)opinionList.Count() / split);

            // 刷新 JwtToken 並送出活動資料
            return Ok(new
            {
                Status = true,
                Data = new
                {
                    TotalPage = totalPageNumber,
                    Opinion = opinionList.Skip(split * (page - 1)).Take(split)
                }
            });
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}