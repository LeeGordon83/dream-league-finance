using LG.DLFinance.Controllers;
using LG.DLFinance.DAL;
using LG.DLFinance.Models;
using LG.DLFinance.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace LG.DLFinance.SL
{
    public class MailService : IMailService
    {

        DLFinanceContext db;

        public MailService()
        {
            this.db = new DLFinanceContext();
        }
        public MailService(DLFinanceContext context)
        {
            this.db = context;
        }

        public void Send(List<EmailViewModel> emailViewModels, ControllerContext context)
        {
            foreach (var emailViewModel in emailViewModels)
            {

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = new System.Net.NetworkCredential("dreamleaguefinance@hotmail.com", "Pitchshifter12~");

                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.Subject = string.Format("Dream League Finances - {0}", emailViewModel.CurrentMonth);

                message.Body = RenderViewToString(context, "EmailView", emailViewModel);
                message.Body = PreMailer.Net.PreMailer.MoveCssInline(message.Body, false, stripIdAndClassAttributes: true).Html;

                var managers = db.Managers.AsNoTracking();

                    foreach (var email in emailViewModel.Manager.Email.Where(x => x.Primary == true))
                    {
                        message.To.Add(email.EmailAddress);
                    }
                    foreach (var email in emailViewModel.Manager.Email.Where(x => x.Primary == false))
                    {
                        message.CC.Add(email.EmailAddress);
                    }


                smtpClient.Send(message);
            }
        }

        private string RenderViewToString(ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = context.RouteData.GetRequiredString("action");
            }

            ViewDataDictionary viewData = new ViewDataDictionary(model);

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                ViewContext viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public void SendCheck()
        {
            string json;

            IWebProxy proxy = WebRequest.DefaultWebProxy;
            proxy.Credentials = CredentialCache.DefaultCredentials;

            using (WebClient client = new WebClient())
            {
                client.Proxy = proxy;
                json = client.DownloadString("https://dreamleaguefantasyfootball.co.uk/api/data/meetings");
            }


            List<MeetingAPI> meetings = JsonConvert.DeserializeObject<List<MeetingAPI>>(json);

            string currentTime = DateTime.UtcNow.ToShortDateString();

            foreach(var meeting in meetings)
            {
                DateTime meetingDateConverted = DateTime.Parse(meeting.MeetingDate);

                string checkDate = meetingDateConverted.AddDays(-3).ToShortDateString();

                if(currentTime == checkDate)
                {
                    var adminController = new AdminController();

                    adminController.Send();
                }
            }
        }
    }
}