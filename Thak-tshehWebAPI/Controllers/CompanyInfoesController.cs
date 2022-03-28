using NSwag.Annotations;
using System.Data;
using System.Linq;
using System.Web.Http;
using Thak_tshehWebAPI.Models;
using Thak_tshehWebAPI.Models.Vms;

namespace Thak_tshehWebAPI.Controllers
{
    /// <summary>
    /// 公司資料服務
    /// </summary>
    [OpenApiTag("CompanyInfoes", Description = "公司資料服務")]
    public class CompanyInfoesController : ApiController
    {
        // 連線資料庫
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/company/infoes
        /// <summary>
        /// 1-6 公司資料
        /// </summary>
        /// <returns></returns>
        [Route("api/company/infoes")]
        [SwaggerResponse(typeof(CompanyInfoVm))]
        [HttpGet]
        public IHttpActionResult GetCompanyInfo()
        {
            var info = db.CompanyInfo.AsNoTracking().Where(x => x.Id == 1).FirstOrDefault();
            CompanyInfoData companyInfoData = new CompanyInfoData();
            companyInfoData.Name = info.Name;
            companyInfoData.Phone = info.Phone;
            companyInfoData.Email = info.Email;
            companyInfoData.FacebookLink = info.FacebookLink;
            companyInfoData.InstagramLink = info.InstagramLink;
            companyInfoData.LineLink = info.LineLink;
            companyInfoData.AboutUs = info.AboutUs;
            companyInfoData.Questions = info.Questions;
            return Ok(new CompanyInfoVm { Status = true, Data = companyInfoData });
        }
    }
}