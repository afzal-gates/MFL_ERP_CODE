using ERP.Model;
using ERPSolution.Controllers;
using FluentScheduler;
using Postal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace ERPSolution
{
    public class TnATaskSummaryMailJob : IJob, IRegisteredObject
    {

       private readonly object _lock = new object();

        private bool _shuttingDown;

        public TnATaskSummaryMailJob()
        {
            HostingEnvironment.RegisterObject(this);
        }
        public static void SendTnASummaryMail(String To, List<TnASummaryMail> TnASumData)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            var email = new TnASummaryMailService
            {
                To = To,
                data = TnASumData

            };

            emailService.Send(email);
        }

        public void Execute()
        {
            lock (_lock)
            {
                if (_shuttingDown)
                    return;
                var obj = new TnASummaryMail().getUserAndMailAddress();
                if (obj.Count > 0)
                {
                    foreach (var item in obj)
                    {
                        var data = new TnASummaryMail().SelectAll(item.SC_USER_ID);
                        if (data.Count > 0)
                        {
                            SendTnASummaryMail("dev.aminul@multi-fabs.com", data);
                        }
                       
                    }
                    HostingEnvironment.UnregisterObject(this);
                }

                
            }
        }

        public void Stop(bool immediate)
        {
            // Locking here will wait for the lock in Execute to be released until this code can continue.
            lock (_lock)
            {
                _shuttingDown = true;
            }

            HostingEnvironment.UnregisterObject(this);
        }
    }
}