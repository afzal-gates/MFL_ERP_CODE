using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
//using ERP.BLL;
using ERPSolution.Common;
using System.Threading;
using Hangfire;
using RazorEngine;
using System.IO;
using System.Web.Hosting;
using Postal;
using Microsoft.AspNet.SignalR;
using ERPSolution.Hubs;
using System.Configuration;
using ERPSolution.Areas.Security.Api;

namespace ERPSolution.Controllers
{
    public class HomeController : BaseController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();
        [SignInCheck]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _ChangePasswordModal()
        {
            return View();
        }

        public PartialViewResult UserDashBoard()
        {
            return PartialView();
        }


        public ActionResult HourlyProdBoard()
        {
            return View();
        }

        public PartialViewResult _HourlyProdBoard()
        {
            return PartialView();
        }

        public ActionResult GmtProductionBoard()
        {
            return View();
        }
        public PartialViewResult _GmtProductionBoard()
        {
            return PartialView();
        }

        public ActionResult LineLoadPlan()
        {
            return View();
        }

        public PartialViewResult _LineLoadPlan()
        {
            return PartialView();
        }
        public ActionResult HourlyProductionEntry()
        {
            return View();
        }

        public PartialViewResult _HourlyProductionEntry()
        {
            return PartialView();
        }

        public PartialViewResult _PartitalManpowerEntry()
        {
            return PartialView();
        }

        public PartialViewResult _HourlyFinProductionEntry()
        {
            return PartialView();
        }

        public PartialViewResult DemoVideo()
        {
            return PartialView();
        }

        
        

        public JsonResult SalaryAdvReqesterNotif()
        {
            var data = new HR_SAL_ADVANCEModel().SalaryAdvReqesterNotif();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SalaryAdvApproverNotif()
        {
            var data = new HR_SAL_ADV_APRVLModel().SalaryAdvApproverNotif();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OnlineLeaveReqesterNotif()
        {
            var data = new HR_LEAVE_TRANSModel().OnlineLeaveReqesterNotif();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OnlineLeaveApproverNotif()
        {
            var data = new LEAVE_DASHBOAD_MODEL().OnlineLeaveApproverNotif();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getNotificationCount()
        {
            var obj = new NotificationCountModel().getNotificationCount();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getMessageData()
        {
            var objList = new RF_NOTIFICATIONModel().getMessageData();
            return Json(objList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getAlertData()
        {
            var objList = new RF_NOTIFICATIONModel().getAlertData();
            return Json(objList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEventData()
        {
            var objList = new RF_NOTIFICATIONModel().getEventData();
            return Json(objList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult seenNotification(Int64 RF_NOTIFICATION_ID)
        {
            string vMsg = "";
            try
            {
                vMsg = new RF_NOTIFICATIONModel().seenNotification(RF_NOTIFICATION_ID);
                Hub.Clients.All.updateNotifCount();
                Hub.Clients.All.showNotification();

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return Json(new { success = true, vMsg });
        }

        public JsonResult seenNotificationByList(String RF_NOTIFICATION_ID_LIST)
        {
            string vMsg = "";
            try
            {
                vMsg = new RF_NOTIFICATIONModel().seenNotificationByList(RF_NOTIFICATION_ID_LIST);
                Hub.Clients.All.updateNotifCount();
                Hub.Clients.All.showNotification();

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return Json(new { success = true, vMsg });
        }

        public JsonResult getNotification()
        {
            var obj = new RF_NOTIFICATIONModel().getNotification();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public static void SendTnASummaryMail(String To,List<TnASummaryMail> TnASumData)
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

        public PartialViewResult _UpdateOrdeShipDataModal()
        {
            return PartialView();
        }

        public JsonResult sentTnaSummaryMail()
        {
            SendTnASummaryMail("dev.aminul@malti-fabs.com",null);
            //var obj = new RF_NOTIFICATIONModel().getNotification();
            //return Json(obj, JsonRequestBehavior.AllowGet);

            return Json(new {SUCCESS = true}, JsonRequestBehavior.AllowGet);
        }

        public static void SendCollarCuffKpGenMail(KNT_PLAN_HModel data)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            var email = new CollarCuffKpGenMailService
            {
                BYR_ACC_NAME_EN = data.BYR_ACC_NAME_EN,
                COLOR_NAME_EN = data.COLOR_NAME_EN,
                CREATED_BY_NAME = data.CREATED_BY_NAME,
                CREATION_DATE = data.CREATION_DATE,
                WORK_STYLE_NO = data.WORK_STYLE_NO,
                ORDER_NO_LIST = data.ORDER_NO_LIST,
                KNT_PLAN_NO = data.KNT_PLAN_NO,
                FAB_TYPE_NAME = data.FAB_TYPE_NAME
            };

            emailService.Send(email);
        }
        public JsonResult sendCollarCuffKpGenMail(Int64 pKNT_PLAN_H_ID)
        {
            var obj = new KNT_PLAN_HModel().getDataByKpIdForMail(pKNT_PLAN_H_ID);
            SendCollarCuffKpGenMail(obj);
            return Json(new { SUCCESS = true }, JsonRequestBehavior.AllowGet);
        }
    }
}