using System.Web.Mvc;

namespace Thak_tshehWebAPI.Controllers
{
    /// <summary>
    /// 前端首頁
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //導回前端首頁
            return Redirect("index.html");
        }
    }
}