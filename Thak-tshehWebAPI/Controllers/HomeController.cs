using System.Web.Mvc;

namespace Thak_tshehWebAPI.Controllers
{
    /// <summary>
    /// 首頁
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 首頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return Redirect("index.html");
        }
    }
}
