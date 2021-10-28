using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Thak_tshehWebAPI.Security;

namespace Thak_tshehWebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //將預設回傳xml清掉
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            // Add a reference here to the new MediaTypeFormatter that adds text/plain support
            //GlobalConfiguration.Configuration.Formatters.Insert(0, new TextMediaTypeFormatter());


            //全部過濾的方式，加了需要在JwtAuthFilter內排除不使用JWT的api，不然沒使用的都會失效
            //GlobalConfiguration.Configuration.Filters.Add(new JwtAuthFilter());
        }
    }
}
