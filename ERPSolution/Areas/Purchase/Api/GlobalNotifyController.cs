using ERPSolution.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Purchase.Api
{
    [RoutePrefix("api/purchase")]
    public class GlobalNotifyController : ApiController
    {

        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();


        [Route("GlobalNotify/showMsg")]
        [HttpGet]
        public IHttpActionResult showMsg(string alertType = null, string heading = null, string msg = null)
        {
            Hub.Clients.All.GlobalNotificationMsg(alertType, heading, msg + "! This action will effect after: " + DateTime.Now.AddMinutes(10).ToString());
            return Ok("Done");
        }
    }
}
