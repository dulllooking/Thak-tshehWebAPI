﻿using Microsoft.Owin;
using NSwag;
using NSwag.AspNet.Owin;
using NSwag.Generation.Processors.Security;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Thak_tshehWebAPI.Startup))]

namespace Thak_tshehWebAPI
{
    /// <summary>
    /// OWIN 啟動類別
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 應用程式配置
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            // 如需如何設定應用程式的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkID=316888
            var config = new HttpConfiguration();
            app.UseSwaggerUi3(typeof(Startup).Assembly, settings =>
            {
                // 針對 WebAPI，指定路由包含 Action 名稱
                settings.GeneratorSettings.DefaultUrlTemplate =
                    "api/{controller}/{action}/{id?}";
                // 加入客製化調整邏輯
                settings.PostProcess = document =>
                {
                    document.Info.Title = "WebAPI : thak-tsheh 作伙來讀冊專題";
                };
                // 加入 Authorization JWT token 定義
                settings.GeneratorSettings.DocumentProcessors.Add(new SecurityDefinitionAppender("Bearer", new OpenApiSecurityScheme()
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "Type into the textbox: Bearer {your JWT token}.",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Scheme = "Bearer" // 不填寫會影響 Filter 判斷錯誤
                }));
                // REF: https://github.com/RicoSuter/NSwag/issues/1304 (每支 API 單獨呈現認證 UI 圖示)
                settings.GeneratorSettings.OperationProcessors.Add(new OperationSecurityScopeProcessor("Bearer"));
            });
            app.UseWebApi(config);
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
        }
    }
}
