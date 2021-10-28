using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;

namespace Thak_tshehWebAPI.Security
{
    public class Mail
    {
        // 生成信箱驗證連結
        public static string SetAuthMailLink(string urlHost, string mailGuid)
        {
            string verifyLink = @"https://" + urlHost + @"/AuthMail?guid=" + mailGuid;

            return verifyLink;
        }

        // 生成忘記密碼重設連結
        public static string SetResetPasswordMailLink(string urlHost, string mailGuid)
        {
            string verifyLink = @"https://" + urlHost + @"/ResetPassword?guid=" + mailGuid;

            return verifyLink;
        }

        //郵件格式正規檢查
        public static bool RegexEmail(string str)
        {
            //郵件格式ccc@kmit.edu.tw
            return Regex.IsMatch(str, @"[a-zA-Z0-9_]+@[a-zA-Z0-9\._]+");
        }

        //密碼格式正規檢查
        public static bool RegexPassword(string str)
        {
            //密碼長度必須有八碼，並且包含至少一個小寫字母與一個大寫字母和一個數字
            return Regex.IsMatch(str, @"^.*(?=.{8,})(?=.*\d)(?=.*[a-zA-Z]).*$");
        }


        /// <summary>
        /// 發送驗證信
        /// </summary>
        /// <param name="toName">收信人名稱</param>
        /// <param name="toAddress">收信地址</param>
        /// <param name="verifyLink">驗證連結</param>
        public static void SendVerifyLinkMail(string toName, string toAddress, string verifyLink)
        {
            string fromAddress = WebConfigurationManager.AppSettings["gmailAccount"];
            string fromName = "作伙來讀冊有限公司";
            string title = "會員認證通知";
            string mailAccount = WebConfigurationManager.AppSettings["gmailAccount"];
            string mailPassword = WebConfigurationManager.AppSettings["gmailPassword"];

            //建立建立郵件
            MimeMessage mail = new MimeMessage();
            // 添加寄件者
            mail.From.Add(new MailboxAddress(fromName, fromAddress));
            // 添加收件者
            mail.To.Add(new MailboxAddress(toName, toAddress.Trim()));
            // 設定郵件標題
            mail.Subject = title;
            //使用 BodyBuilder 建立郵件內容
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody =
                "<h1>作伙來讀冊-帳號開通連結</h1>" +
                $"<h3>請點選以下連結進行帳號開通登入，如未開通帳號將無法使用網站進階功能!</h3>" +
                $"<a href='{verifyLink}'>{verifyLink}</a>";
            //設定郵件內容
            mail.Body = bodyBuilder.ToMessageBody(); //轉成郵件內容格式

            using (var client = new SmtpClient()) {
                //有開防毒時需設定關閉檢查
                client.CheckCertificateRevocation = false;
                //設定連線 gmail ("smtp Server", Port, SSL加密) 
                client.Connect("smtp.gmail.com", 587, false); // localhost 測試使用加密需先關閉 

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(mailAccount, mailPassword);

                client.Send(mail);
                client.Disconnect(true);
            }
        }

        /// <summary>
        /// 發送驗證信
        /// </summary>
        /// <param name="toName">收信人名稱</param>
        /// <param name="toAddress">收信地址</param>
        /// <param name="resetLink">重設連結</param>
        public static void SendResetLinkMail(string toName, string toAddress, string resetLink)
        {
            string fromAddress = WebConfigurationManager.AppSettings["gmailAccount"];
            string fromName = "作伙來讀冊有限公司";
            string title = "密碼重設通知";
            string mailAccount = WebConfigurationManager.AppSettings["gmailAccount"];
            string mailPassword = WebConfigurationManager.AppSettings["gmailPassword"];

            //建立建立郵件
            MimeMessage mail = new MimeMessage();
            // 添加寄件者
            mail.From.Add(new MailboxAddress(fromName, fromAddress));
            // 添加收件者
            mail.To.Add(new MailboxAddress(toName, toAddress.Trim()));
            // 設定郵件標題
            mail.Subject = title;
            //使用 BodyBuilder 建立郵件內容
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody =
                "<h1>作伙來讀冊-密碼重設連結</h1>" +
                $"<h3>請點選以下連結進行密碼重設!</h3>" +
                $"<a href='{resetLink}'>{resetLink}</a>";
            //設定郵件內容
            mail.Body = bodyBuilder.ToMessageBody(); //轉成郵件內容格式

            using (var client = new SmtpClient()) {
                //有開防毒時需設定關閉檢查
                client.CheckCertificateRevocation = false;
                //設定連線 gmail ("smtp Server", Port, SSL加密) 
                client.Connect("smtp.gmail.com", 587, false); // localhost 測試使用加密需先關閉 

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(mailAccount, mailPassword);

                client.Send(mail);
                client.Disconnect(true);
            }
        }

        /// <summary>
        /// 發送聯絡我們訊息
        /// </summary>
        /// <param name="toName">收信人名稱</param>
        /// <param name="toAddress">收信地址</param>
        /// <param name="message">驗證連結</param>
        public static void SendMessageToUs(string toName, string toAddress, string userName, string userAccount, string userMessage)
        {
            string fromAddress = WebConfigurationManager.AppSettings["gmailAccount"];
            string fromName = "作伙來讀冊有限公司";
            string title = "會員站內反應信";
            string mailAccount = WebConfigurationManager.AppSettings["gmailAccount"];
            string mailPassword = WebConfigurationManager.AppSettings["gmailPassword"];

            //建立建立郵件
            MimeMessage mail = new MimeMessage();
            // 添加寄件者
            mail.From.Add(new MailboxAddress(fromName, fromAddress));
            // 添加收件者
            mail.To.Add(new MailboxAddress(toName, toAddress.Trim()));
            // 設定郵件標題
            mail.Subject = title;
            //使用 BodyBuilder 建立郵件內容
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody =
                "<h1>作伙來讀冊-會員站內訊息</h1>" +
                $"<h3>會員姓名 : {userName}</h3>" +
                $"<h3>會員帳號 : {userAccount}</h3>" +
                $"<h3>留言內容 : </h3>" +
                $"<P>{userMessage}</a>";
            //設定郵件內容
            mail.Body = bodyBuilder.ToMessageBody(); //轉成郵件內容格式

            using (var client = new SmtpClient()) {
                //有開防毒時需設定關閉檢查
                client.CheckCertificateRevocation = false;
                //設定連線 gmail ("smtp Server", Port, SSL加密) 
                client.Connect("smtp.gmail.com", 587, false); // localhost 測試使用加密需先關閉 

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(mailAccount, mailPassword);

                client.Send(mail);
                client.Disconnect(true);
            }
        }
    }
}