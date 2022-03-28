using NSwag.Annotations;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using Thak_tshehWebAPI.Models;
using Thak_tshehWebAPI.Models.Attributes;
using Thak_tshehWebAPI.Models.Params;
using Thak_tshehWebAPI.Models.Vms;

namespace Thak_tshehWebAPI.Controllers
{
    //[EnableCors("*", "*", "*")] // 官方跨域設定加入 Owin Swagger UI 生成 Startup.cs 導致失效
    /// <summary>
    /// 活動相關服務
    /// </summary>
    [OpenApiTag("Activities", Description = "活動相關服務")]
    public class ActivitiesController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/activities/top-views
        /// <summary>
        /// 1-1 熱門讀書會資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(ActivityViewsVm))]
        [Route("api/activities/top-views")]
        public IHttpActionResult GetActivityTop()
        {
            var infoType0 = db.Activity.AsNoTracking().Where(x => ((int)x.ActivityType) == 0).OrderByDescending(x => x.Views).Select(x => new ActivityViewsData
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                ActivityStartDate = x.ActivityStartDate,
                ActivityEndDate = x.ActivityEndDate,
                LimitNumber = x.LimitNumber,
                Summary = x.Summary,
                OrganizerName = x.OrganizerName,
                ActivityType = x.ActivityType,
                Views = x.Views,
                CreatDate = x.CreatDate
            }).FirstOrDefault();

            var infoType1 = db.Activity.AsNoTracking().Where(x => ((int)x.ActivityType) == 1).OrderByDescending(x => x.Views).Select(x => new ActivityViewsData
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                ActivityStartDate = x.ActivityStartDate,
                ActivityEndDate = x.ActivityEndDate,
                LimitNumber = x.LimitNumber,
                Summary = x.Summary,
                OrganizerName = x.OrganizerName,
                ActivityType = x.ActivityType,
                Views = x.Views,
                CreatDate = x.CreatDate
            }).FirstOrDefault();

            var infoType2 = db.Activity.AsNoTracking().Where(x => ((int)x.ActivityType) == 2).OrderByDescending(x => x.Views).Select(x => new ActivityViewsData
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                ActivityStartDate = x.ActivityStartDate,
                ActivityEndDate = x.ActivityEndDate,
                LimitNumber = x.LimitNumber,
                Summary = x.Summary,
                OrganizerName = x.OrganizerName,
                ActivityType = x.ActivityType,
                Views = x.Views,
                CreatDate = x.CreatDate
            }).FirstOrDefault();

            // 轉成陣列
            ActivityViewsData[] dataArray = new ActivityViewsData[3] { infoType0, infoType1, infoType2 };

            return Ok(new ActivityViewsVm { Status = true, Data = dataArray });
        }

        // GET: api/activities/new9/type/類型(0~2)
        /// <summary>
        /// 1-2 1-3 1-4 滾動區資料
        /// </summary>
        /// <param name="type">類型(0~2)</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(ActivityViewsVm))]
        [Route("api/activities/new9/type/{type}")]
        public IHttpActionResult GetNew9Activity(int type)
        {
            var info = db.Activity.AsNoTracking().Where(x => ((int)x.ActivityType) == type).OrderByDescending(x => x.CreatDate).Select(x => new ActivityViewsData
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                ActivityStartDate = x.ActivityStartDate,
                ActivityEndDate = x.ActivityEndDate,
                LimitNumber = x.LimitNumber,
                Summary = x.Summary,
                OrganizerName = x.OrganizerName,
                ActivityType = x.ActivityType,
                Views = x.Views,
                CreatDate = x.CreatDate
            }).Take(9).ToArray();

            return Ok(new ActivityViewsVm { Status = true, Data = info });
        }

        // GET: api/activity/search/每頁幾筆/第幾頁/類型/分類/地區/排序/關鍵字 (用空格區隔最多可查兩個關鍵字)
        /// <summary>
        /// 1-7 Navbar 搜尋功能
        /// </summary>
        /// <param name="searchActivityParam">活動搜尋參數</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(SearchActivityVm))]
        [Route("api/activity/search/{split:int?}/{page:int?}/{type:int?}/{classify:int?}/{area:int?}/{sorting:int?}/{query?}")]
        public IHttpActionResult GetSearchActivity([FromUri] SearchActivityParam searchActivityParam)
        {
            SearchActivityParam param = new SearchActivityParam();
            param = searchActivityParam;
            // 前端會使用encodeURI(關鍵字)，因此需要解碼
            string decodeKeyWord = HttpUtility.UrlDecode(param.Query).Trim();
            // 切割取出兩組關鍵字
            string[] keyWordArr = decodeKeyWord.Split(' ');
            string keyWord1 = keyWordArr[0].ToString();
            // 只有一個關鍵字時，就查詢奇怪的東西
            string keyWord2 = keyWordArr.Length > 1 ? keyWordArr[1].ToString() : "~!@#$%^&*()_+";

            // var info = db.Activity; // 沒加查詢語句時型別是 DBset 後面無法再取出查詢
            // 多個獨立條件需分開用.Where()，不然結果不是預期的東西
            var info = db.Activity.AsNoTracking().Where(x => param.Type < 0 || (int)x.ActivityType == param.Type)
                .Where(x => param.Classify < 0 || (int)x.ActivityClass == param.Classify)
                .Where(x => (x.Name.Contains(keyWord1) || x.OrganizerName.Contains(keyWord1) || x.Summary.Contains(keyWord1) || x.ContentText.Contains(keyWord1)) || (x.Name.Contains(keyWord2) || x.OrganizerName.Contains(keyWord2) || x.Summary.Contains(keyWord2) || x.ContentText.Contains(keyWord2)))
                .Where(x => param.Area == 0? (int)x.Area <= 6 : param.Area == 1? ((int)x.Area > 6 || (int)x.Area <= 11) : param.Area == 2? ((int)x.Area > 11 || (int)x.Area <= 16) : param.Area == 3? ((int)x.Area > 16 || (int)x.Area <= 18) : param.Area == 4 ? (int)x.Area > 18 : (int)x.Area >= 0)
                .Select(x => new ActivityData
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                ActivityStartDate = x.ActivityStartDate,
                ActivityEndDate = x.ActivityEndDate,
                LimitNumber = x.LimitNumber,
                Summary = x.Summary,
                OrganizerName = x.OrganizerName,
                ActivityType = x.ActivityType,
                ActivityClass = x.ActivityClass,
                ActivityArea = x.Area,
                Views = x.Views,
                ApplicantNumber = x.ApplicantNumber,
                CollectNumber = x.CollectNumber,
                EvaluateStars = x.EvaluateStars,
                OpinionNumber = x.OpinionNumber,
                Price = x.Price,
                StartAcceptDate = x.StartAcceptDate,
                EndAcceptDate = x.EndAcceptDate
            });

            // 排序功能
            switch (param.Sorting) {
                case 0: // 最新發佈 (預設) => 以活動開始時間排序，由近到遠
                    info = info.OrderByDescending(x => x.StartAcceptDate);
                    break;
                case 1: // 最熱門活動 => 以瀏覽人數排序，由多到少
                    info = info.OrderByDescending(x => x.Views);
                    break;
                case 2: // 日期由新到舊 => 以開放報名時間排序，由近到遠
                    info = info.OrderBy(x => x.ActivityStartDate);
                    break;
                default:
                    break;
            }

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)info.Count() / param.Split.Value);

            // .Skip() 前一定要接 .OrderBy()
            return Ok(new SearchActivityVm
            {
                Status = true,
                Data = new SearchActivityData
                {
                    TotalPage = totalPageNumber,
                    Activity = info.Skip(param.Split.Value * (param.Page.Value - 1)).Take(param.Split.Value).ToArray()
                }
            });
        }

        // GET: api/activity/final/type/類型/每頁幾筆/第幾頁
        /// <summary>
        /// 4-1 即將截止報名資料
        /// </summary>
        /// <param name="recommendActivityParam">官方推薦參數</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(SearchActivityVm))]
        [Route("api/activity/final/type/{type:int?}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetEndAcceptsSoonActivity([FromUri] RecommendActivityParam recommendActivityParam)
        {
            RecommendActivityParam param = new RecommendActivityParam();
            param = recommendActivityParam;
            var info = db.Activity.AsNoTracking().Where(x => (int)x.ActivityType == param.Type.Value && x.EndAcceptDate > DateTime.Now && !x.ApplicantFull).Select(x => new ActivityData
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                ActivityStartDate = x.ActivityStartDate,
                ActivityEndDate = x.ActivityEndDate,
                LimitNumber = x.LimitNumber,
                Summary = x.Summary,
                OrganizerName = x.OrganizerName,
                ActivityType = x.ActivityType,
                ActivityClass = x.ActivityClass,
                ActivityArea = x.Area,
                Views = x.Views,
                ApplicantNumber = x.ApplicantNumber,
                CollectNumber = x.CollectNumber,
                EvaluateStars = x.EvaluateStars,
                OpinionNumber = x.OpinionNumber,
                Price = x.Price,
                StartAcceptDate = x.StartAcceptDate,
                EndAcceptDate = x.EndAcceptDate
            }).OrderBy(x => x.EndAcceptDate);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)info.Count() / param.Split.Value);

            return Ok(new SearchActivityVm
            {
                Status = true,
                Data = new SearchActivityData
                {
                    TotalPage = totalPageNumber,
                    Activity = info.Skip(param.Split.Value * (param.Page.Value - 1)).Take(param.Split.Value).ToArray()
                }
            });
        }

        // GET: api/activity/hot/type/類型/每頁幾筆/第幾頁
        /// <summary>
        /// 4-2 最多人報名資料
        /// </summary>
        /// <param name="recommendActivityParam">官方推薦參數</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(SearchActivityVm))]
        [Route("api/activity/hot/type/{type:int?}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetHotActivity([FromUri] RecommendActivityParam recommendActivityParam)
        {
            RecommendActivityParam param = new RecommendActivityParam();
            param = recommendActivityParam;
            var info = db.Activity.AsNoTracking().Where(x => (int)x.ActivityType == param.Type.Value && x.EndAcceptDate > DateTime.Now && !x.ApplicantFull).Select(x => new ActivityData
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                ActivityStartDate = x.ActivityStartDate,
                ActivityEndDate = x.ActivityEndDate,
                LimitNumber = x.LimitNumber,
                Summary = x.Summary,
                OrganizerName = x.OrganizerName,
                ActivityType = x.ActivityType,
                ActivityClass = x.ActivityClass,
                ActivityArea = x.Area,
                Views = x.Views,
                ApplicantNumber = x.ApplicantNumber,
                CollectNumber = x.CollectNumber,
                EvaluateStars = x.EvaluateStars,
                OpinionNumber = x.OpinionNumber,
                Price = x.Price,
                StartAcceptDate = x.StartAcceptDate,
                EndAcceptDate = x.EndAcceptDate
            }).OrderByDescending(x => x.ApplicantNumber);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)info.Count() / param.Split.Value);

            return Ok(new SearchActivityVm
            {
                Status = true,
                Data = new SearchActivityData
                {
                    TotalPage = totalPageNumber,
                    Activity = info.Skip(param.Split.Value * (param.Page.Value - 1)).Take(param.Split.Value).ToArray()
                }
            });
        }

        // GET: api/activity/new/type/類型/每頁幾筆/第幾頁
        /// <summary>
        /// 4-3 新推出資料
        /// </summary>
        /// <param name="recommendActivityParam">官方推薦參數</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(SearchActivityVm))]
        [Route("api/activity/new/type/{type:int?}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetNewActivity([FromUri] RecommendActivityParam recommendActivityParam)
        {
            RecommendActivityParam param = new RecommendActivityParam();
            param = recommendActivityParam;
            var info = db.Activity.AsNoTracking().Where(x => (int)x.ActivityType == param.Type.Value && x.EndAcceptDate > DateTime.Now && !x.ApplicantFull).Select(x => new ActivityData
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                ActivityStartDate = x.ActivityStartDate,
                ActivityEndDate = x.ActivityEndDate,
                LimitNumber = x.LimitNumber,
                Summary = x.Summary,
                OrganizerName = x.OrganizerName,
                ActivityType = x.ActivityType,
                ActivityClass = x.ActivityClass,
                ActivityArea = x.Area,
                Views = x.Views,
                ApplicantNumber = x.ApplicantNumber,
                CollectNumber = x.CollectNumber,
                EvaluateStars = x.EvaluateStars,
                OpinionNumber = x.OpinionNumber,
                Price = x.Price,
                StartAcceptDate = x.StartAcceptDate,
                EndAcceptDate = x.EndAcceptDate
            }).OrderByDescending(x => x.StartAcceptDate);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)info.Count() / param.Split.Value);

            return Ok(new SearchActivityVm
            {
                Status = true,
                Data = new SearchActivityData
                {
                    TotalPage = totalPageNumber,
                    Activity = info.Skip(param.Split.Value * (param.Page.Value - 1)).Take(param.Split.Value).ToArray()
                }
            });
        }

        // GET: api/activity/id/活動ID
        /// <summary>
        /// 5-1 活動+舉辦者資料
        /// </summary>
        /// <param name="id">活動ID</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(ActivityDetailVm))]
        [Route("api/activity/id/{id}")]
        public IHttpActionResult GetActivityDataById(int id)
        {
            // 查詢活動舉辦記錄
            var organizerData = db.OrganizerLog.AsNoTracking().Where(x => x.ActivityId == id).Select(x => new OrganizerData
            {
                Id = x.User.Id,
                Account = x.User.Account,
                Image = x.User.Image,
                Name = x.User.Name,
                NickName = x.User.NickName,
                AboutMe = x.User.AboutMe,
                Views = x.User.Views,
                FacebookLink = x.User.FacebookLink,
                InstagramLink = x.User.InstagramLink,
                CreatDate = x.User.CreatDate
            }).FirstOrDefault();

            // 查詢指定活動
            var activityData = db.Activity.FirstOrDefault(x => x.Id == id);

            return Ok(new ActivityDetailVm { Status = true, Data = new ActivityDetailData { ActivityData = activityData, OrganizerData = organizerData } });
        }

        // PUT: api/organizer/activity/views
        /// <summary>
        /// 5-2 增加活動瀏覽次數
        /// </summary>
        /// <param name="activityData">活動基本資料</param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerResponse(typeof(ApiMessageResult))]
        [Route("api/organizer/activity/views")]
        public IHttpActionResult AddActivityViews(ActivityDataVm activityData)
        {
            try {
                // 查詢指定活動
                var activityQuery = db.Activity.FirstOrDefault(x => x.Id == activityData.ActivityId);

                // 增加瀏覽次數
                activityQuery.Views += 1;
                db.SaveChanges();
            }
            catch (Exception ex) {

                return Ok(new ApiMessageResult { Status = false, Message = ex.Message });
            }

            return Ok(new ApiMessageResult { Status = true, Message = "活動瀏覽次數增加成功" });
        }

        // GET: api/activity/opinion/活動ID/每頁幾筆/第幾頁
        /// <summary>
        /// 5-3 活動評價資料+分頁
        /// </summary>
        /// <param name="activityOpinionParam">評價取得參數</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(ActivityOpinionListVm))]
        [Route("api/activity/opinion/{id}/{split:int?}/{page:int?}")]
        public IHttpActionResult GetActivityOpinionList(ActivityOpinionParam activityOpinionParam)
        {
            ActivityOpinionParam param = new ActivityOpinionParam();
            param = activityOpinionParam;

            // 查詢活動評價記錄
            var opinionList = db.ActivityOpinion.AsNoTracking().Where(x => x.ActivityId == param.Id).Select(x => new OpinionData
            {
                UserId = x.UserId,
                Image = x.User.Image,
                Name = x.User.Name,
                CreatDate = x.CreatDate,
                Star = x.Star,
                Opinion = x.Opinion
            }).OrderByDescending(x => x.CreatDate);

            // 計算總頁數
            int totalPageNumber = (int)Math.Ceiling((float)opinionList.Count() / param.Split.Value);

            return Ok(new ActivityOpinionListVm
            {
                Status = true,
                Data = new ActivityOpinionData
                {
                    TotalPage = totalPageNumber,
                    Opinion = opinionList.Skip(param.Split.Value * (param.Page.Value - 1)).Take(param.Split.Value).ToArray()
                }
            });
        }
    }
}