using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using RestSharp;
using System.Web.SessionState;

namespace RegistForm
{
    /// <summary>
    /// 報名頁面API
    /// </summary>
    public class registform : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //token失敗就擋掉
            bool verification = bool.Parse(context.Session["verification"].ToString());

            if(verification != true)
            {
                context.Response.Write("機器人母湯來喔");
                return;
            }

            string name = context.Request.Form["name"];
            string email = context.Request.Form["email"];
            string tel = context.Request.Form["tel"];
            string introduction = context.Request.Form["introduction"];
            string why = context.Request.Form["why"];
            string essay = context.Request.Form["essay"];
            string joingroup = context.Request.Form["joingroup"];
            string batch = context.Request.Form["batch"];
            string pdf = "";

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(tel) || string.IsNullOrEmpty(introduction) || string.IsNullOrEmpty(why) || string.IsNullOrEmpty(essay) || string.IsNullOrEmpty(joingroup) || string.IsNullOrEmpty(batch))
            {
                context.Response.Write("欄位未填寫完整");
                return;
            }

            if (context.Request.Files.Count == 0)
            {
                context.Response.Write("請上傳檔案");
                return;
            }

            //取得檔案
            HttpPostedFile file = context.Request.Files[0];
   
            // 取得副檔名
            string Extension = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            //pdf檔名取姓名+年月日時分秒
            pdf = String.Format("{0}{1:yyyyMMddhhmmsss}.{2}", name, DateTime.Now, Extension);

            //限制上傳pdf
            if (pdf.IndexOf("pdf") == -1)
            {
                context.Response.Write("檔案型態錯誤");
                return;
            }

            //用SaveAs的方法上傳圖片到指定的資料夾
            file.SaveAs(context.Server.MapPath("/Upload/" + pdf));

            //寫入資料庫
            string regist = System.Web.Configuration.WebConfigurationManager
                    .ConnectionStrings["RegistConnectionString"].ToString();
            SqlConnection registConnection = new SqlConnection(regist);
            SqlCommand registCommand =
                new SqlCommand(
                    $"INSERT INTO registform (name,email,tel,pdf,introduction,why,essay,joingroup,batch) VALUES(@name,@email,@tel,@pdf,@introduction,@why,@essay,@joingroup,@batch)",
                    registConnection);
            registCommand.Parameters.Add("@name", SqlDbType.NVarChar);
            registCommand.Parameters["@name"].Value = name;
            registCommand.Parameters.Add("@email", SqlDbType.NVarChar);
            registCommand.Parameters["@email"].Value = email;
            registCommand.Parameters.Add("@tel", SqlDbType.NVarChar);
            registCommand.Parameters["@tel"].Value = tel;
            registCommand.Parameters.Add("@pdf", SqlDbType.NVarChar);
            registCommand.Parameters["@pdf"].Value = pdf;
            registCommand.Parameters.Add("@introduction", SqlDbType.NVarChar);
            registCommand.Parameters["@introduction"].Value = introduction;
            registCommand.Parameters.Add("@why", SqlDbType.NVarChar);
            registCommand.Parameters["@why"].Value = why;
            registCommand.Parameters.Add("@essay", SqlDbType.NVarChar);
            registCommand.Parameters["@essay"].Value = essay;
            registCommand.Parameters.Add("@joingroup", SqlDbType.NVarChar);
            registCommand.Parameters["@joingroup"].Value = joingroup;
            registCommand.Parameters.Add("@batch", SqlDbType.NVarChar);
            registCommand.Parameters["@batch"].Value = batch;
            registConnection.Open();
            registCommand.ExecuteNonQuery();
            registConnection.Close();

            context.Response.Write("success");
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }



        public class ResponseToken
        {

            public DateTime challenge_ts { get; set; }
            public float score { get; set; }
            public string action { get; set; }
            public bool success { get; set; }
            public string hostname { get; set; }
        }


    }
}