using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Thak_tshehWebAPI.Models;

namespace Thak_tshehWebAPI.Controllers
{
    // 啟用跨網域存取
    [EnableCors("*", "*", "*")]
    public class ActivitiesController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/activities/new9/type/0~2
        [Route("api/activities/new9/type/{type}")]
        [HttpGet]
        public IHttpActionResult GetActivity(int type)
        {
            var info = db.Activity.Where(x => ((int)x.ActivityType) == type).OrderByDescending(x => x.CreatDate).Take(9);
            List<Activity> infoList = info.ToList();
            var data = infoList.Select(x => new
            {
                x.Name,
                x.Image,
                x.ActivityStartDate,
                x.ActivityEndDate,
                x.LimitNumber,
                x.Summary,
                x.Views
            });

            return Json(data);
        }

        // GET: api/activities/top-views
        [Route("api/activities/top-views")]
        [HttpGet]
        public IHttpActionResult GetActivityTop()
        {
            var infoType0 = db.Activity.Where(x => ((int)x.ActivityType) == 0).OrderByDescending(x => x.Views).Take(1);
            List<Activity> infoType0List = infoType0.ToList();
            var dataType0 = infoType0List.Select(x => new
            {
                x.Name,
                x.Image,
                x.ActivityStartDate,
                x.ActivityEndDate,
                x.LimitNumber,
                x.Summary,
                x.Views
            }).FirstOrDefault();

            var infoType1 = db.Activity.Where(x => ((int)x.ActivityType) == 1).OrderByDescending(x => x.Views).Take(1);
            List<Activity> infoType1List = infoType1.ToList();
            var dataType1 = infoType1List.Select(x => new
            {
                x.Name,
                x.Image,
                x.ActivityStartDate,
                x.ActivityEndDate,
                x.LimitNumber,
                x.Summary,
                x.Views
            }).FirstOrDefault();

            var infoType2 = db.Activity.Where(x => ((int)x.ActivityType) == 2).OrderByDescending(x => x.Views).Take(1);
            List<Activity> infoType2List = infoType2.ToList();
            var dataType2 = infoType2List.Select(x => new
            {
                x.Name,
                x.Image,
                x.ActivityStartDate,
                x.ActivityEndDate,
                x.LimitNumber,
                x.Summary,
                x.Views
            }).FirstOrDefault();

            return Json(new { 熱門線上讀書會 = dataType0, 熱門實體讀書會 = dataType1, 熱門活動工作坊 = dataType2 });
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