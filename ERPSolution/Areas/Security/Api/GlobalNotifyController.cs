using ERP.Model;
using ERPSolution.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Serialization;

namespace ERPSolution.Areas.Security.Api
{
    [RoutePrefix("api/security")]
    public class GlobalNotifyController : ApiController
    {

        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        string stringKey = "";

        string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/UPLOAD_DOCS/GlobalMessage.txt");

        [Route("GlobalNotify/showMsg")]
        [HttpGet]
        public IHttpActionResult showMsg(string alertType = null, string heading = null, string msg = null, int msgTime = 10, int offline = 10)
        {
            globalNofification.alertType = alertType;
            globalNofification.title = heading;
            globalNofification.vMsg = msg;
            globalNofification.msgTimeOut = DateTime.Now.AddMinutes(msgTime).ToString();
            globalNofification.offlineTime = Convert.ToDateTime(globalNofification.msgTimeOut).AddMinutes(offline);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("msgTimeOut=" + globalNofification.msgTimeOut);
                writer.WriteLine("offlineTime=" + globalNofification.offlineTime);
            }

            Hub.Clients.All.GlobalNotificationMsg(alertType, heading, msg);
            //Hub.Clients.All.GlobalNotificationMsg(alertType, heading, msg + "! This action will effect after: " + DateTime.Now.AddMinutes(10).ToString());
            return Ok("Done");
        }


        [Route("GlobalNotify/GetMsg")]
        [HttpGet]
        public IHttpActionResult GetMsg()
        {
            if (globalNofification.alertType == null)
            {
                if (File.Exists(filePath))
                {
                    string[] lines = System.IO.File.ReadAllLines(filePath);

                    for (int i = 0; i < lines.Count(); i++)
                    {
                        if (i == 0)
                            globalNofification.msgTimeOut = lines[i].Split('=')[1];
                        else
                            globalNofification.offlineTime = Convert.ToDateTime(lines[i].Split('=')[1]);
                    }
                }
            }

            int page = 1;
            if (globalNofification.msgTimeOut != "")
                if (Convert.ToDateTime(globalNofification.msgTimeOut) < DateTime.Now && globalNofification.offlineTime > DateTime.Now)
                {
                    page = 0;
                }

            return Ok(new { globalNofification.alertType, globalNofification.title, globalNofification.vMsg, globalNofification.msgTimeOut, page });

        }

        [Route("GlobalNotify/OfflineMsg")]
        [HttpGet]
        public IHttpActionResult OfflineMsg()
        {

            var data = globalNofification.offlineTime;
            return Ok(data);

        }

        [Route("GlobalNotify/ClearMsg")]
        [HttpGet]
        public IHttpActionResult ClearMsg()
        {
            globalNofification.alertType = "";
            globalNofification.title = "";
            globalNofification.vMsg = "";
            globalNofification.msgTimeOut = "";
            globalNofification.offlineTime = DateTime.Now;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            Hub.Clients.All.GlobalNotificationMsg("", "", "");
            return Ok("");
        }

        [Route("GlobalNotify/CheckUserSession")]
        [HttpGet]
        public IHttpActionResult CheckUserSession()
        {
            var user = new ScUserModel().CheckUserSession();
            return Ok(user);
        }


    }


    public static class globalNofification
    {
        public static string alertType { get; set; }
        public static string title { get; set; }
        public static string vMsg { get; set; }
        public static string msgTimeOut { get; set; }
        public static DateTime offlineTime { get; set; }


    }
}
