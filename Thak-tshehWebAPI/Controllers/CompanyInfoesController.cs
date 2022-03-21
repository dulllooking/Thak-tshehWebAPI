using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Thak_tshehWebAPI.Models;
using Thak_tshehWebAPI.Security;

namespace Thak_tshehWebAPI.Controllers
{
    public class CompanyInfoesController : ApiController
    {
        // 連線資料庫
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // 1-6 公司資料
        // GET: api/company/infoes
        [Route("api/company/infoes")]
        [HttpGet]
        public IHttpActionResult GetCompanyInfo()
        {
            var info = db.CompanyInfo.Where(x => x.Id == 1);
            List<CompanyInfo> infoList = info.ToList();
            var data = infoList.Select(x => new
            {
                x.Name,
                x.Phone,
                x.Email,
                x.FacebookLink,
                x.InstagramLink,
                x.LineLink,
                x.AboutUs,
                x.Questions
            }).FirstOrDefault();
            return Json(new { Status = true, Data = data });
        }

        // Test
        [Route("api/test/return")]
        [HttpPost]
        public HttpResponseMessage TestReturnInfo([FromBody] NewebPayReturn result)
        {
            CompanyInfo companyInfo = new CompanyInfo
            {
                Name = "Return",
                AboutUs = result.TradeInfo,
                CreatDate = DateTime.Now
            };
            db.CompanyInfo.Add(companyInfo);
            db.SaveChanges();
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent("ReturnSuccess!");    // 回應內容
            return response;
        }

        // Test
        [Route("api/test/notify")]
        [HttpPost]
        public HttpResponseMessage TestNotifyInfo([FromBody] NewebPayReturn result)
        {
            CompanyInfo companyInfo = new CompanyInfo
            {
                Name = "Notify",
                Questions = result.TradeInfo,
                CreatDate = DateTime.Now
            };
            db.CompanyInfo.Add(companyInfo);
            db.SaveChanges();
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StringContent("NotifySuccess!");    // 回應內容
            return response;
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