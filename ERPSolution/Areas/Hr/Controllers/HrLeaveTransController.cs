using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;
using System.Collections;
using Microsoft.AspNet.SignalR;
using ERPSolution.Hubs;
using System.IO;
using System.Web.Hosting;
using Postal;
using Hangfire;
using System.Configuration;
using System.Data.OracleClient;

namespace ERPSolution.Areas.Hr.Controllers
{
    [SignInCheck]
    public class HrLeaveTransController : BaseController
    {

        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();
        public ActionResult Index(Int64? a = 0)
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); 
            
            ViewBag.LK_ACTION_TYPE_ID = a;

            var obList = new HR_LEAVE_TRANSModel().SelectAll();
            return View(obList);
        }
        public JsonResult ListData()
        {
            var obList = new HR_LEAVE_TYPEModel().SelectAll();
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompanyData()
        {
            var obList = new HrCompanyModel().CompanyListData();
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FiscalYearData()
        {
            var obList = new RF_FISCAL_YEARModel().FiscalYearData();
            return Json(obList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult FiscalYearDataAll()
        {
            var obList = new RF_FISCAL_YEARModel().FiscalYearDataAll();
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LookupListData(Int64 id)
        {
            var obList = new LookupDataModel().LookupListData(id);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LeaveBalance(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, Int64 HR_EMPLOYEE_ID)
        {
            var obList = new HR_LEAVE_BALModel().LeaveBalance(HR_COMPANY_ID, RF_FISCAL_YEAR_ID, HR_EMPLOYEE_ID);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LeaveBalanceByType(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, Int64 HR_LEAVE_TYPE_ID)
        {
            var obList = new HR_LEAVE_BALModel().LeaveBalanceByType(HR_COMPANY_ID, RF_FISCAL_YEAR_ID, HR_LEAVE_TYPE_ID);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }



        public JsonResult LeaveBalanceByTypeEmp(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, Int64 HR_EMPLOYEE_ID, Int64 HR_LEAVE_TYPE_ID)
        {
            Int64 Balance = new HR_LEAVE_BALModel().LeaveBalanceByTypeEmp(HR_COMPANY_ID, RF_FISCAL_YEAR_ID, HR_EMPLOYEE_ID, HR_LEAVE_TYPE_ID);
            return Json(Balance, JsonRequestBehavior.AllowGet);
        }


        public JsonResult findNextWorkingDay(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, DateTime TO_DATE, Int64 HR_EMPLOYEE_ID)
        {
            DateTime NextJoinDate = new HR_LEAVE_BALModel().findNextWorkingDay(HR_COMPANY_ID, RF_FISCAL_YEAR_ID, TO_DATE, HR_EMPLOYEE_ID);
            return Json(NextJoinDate, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DateDiff(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, Int64 HR_LEAVE_TYPE_ID, DateTime FROM_DATE, DateTime TO_DATE, Int64 HR_EMPLOYEE_ID)
        {
            Object DateDiff = new HR_LEAVE_BALModel().DateDiff(HR_COMPANY_ID, RF_FISCAL_YEAR_ID, HR_LEAVE_TYPE_ID, FROM_DATE, TO_DATE, HR_EMPLOYEE_ID);
            return Json(DateDiff, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OffDayLeaveData(HR_LEAVE_TRANSModel ob)
        {
            var obList = new HR_LEAVE_TRANSModel().OffDayLeaveData(ob);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getLeaveData(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, Int64 HR_EMPLOYEE_ID)
        {
            var obList = new HR_LEAVE_TRANSModel().GetLeaveData(HR_COMPANY_ID, RF_FISCAL_YEAR_ID, HR_EMPLOYEE_ID);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Update(HR_LEAVE_TRANSModel ob)
        {
         
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Update();

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            else
            {
                var errors = new Hashtable();
                foreach (var pair in ModelState)
                {
                    if (pair.Value.Errors.Count > 0)
                    {
                        errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
                    }
                }
                return Json(new { success = false, errors });
            }

            return Json(new { success = true, jsonStr });

        }


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Save(HR_LEAVE_TRANSModel ob)
        {
      
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = new HR_LEAVE_TRANSModel().Save(ob);

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            else
            {
                var errors = new Hashtable();
                foreach (var pair in ModelState)
                {
                    if (pair.Value.Errors.Count > 0)
                    {
                        errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
                    }
                }
                return Json(new { success = false, errors });
            }

            return Json(new { success = true, jsonStr });
        }


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult uploadImage(HR_LEAVE_TRANSModel ob)
        {
            string extension = null;
            if (ob.ATT_FILE != null && ob.ATT_FILE.ContentLength > 0)
            {
                extension = Path.GetExtension(ob.ATT_FILE.FileName);
                string path = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/SICK_LV_DOCS"), ob.DOC_PATH_REF);
                ob.ATT_FILE.SaveAs(path);
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult removeImage(HR_LEAVE_TRANSModel ob)
        {
            if (ob.ATT_FILE != null && ob.ATT_FILE.ContentLength > 0)
            {
                string path = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/SICK_LV_DOCS"), ob.DOC_PATH_REF);
                System.IO.File.Delete(path);
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult saveDataForEleave(HR_LEAVE_TRANSModel ob, Int64 pOption)
        {
            String PushMessage = "A leave request from " + ob.EMP_FULL_NAME_EN + ", " + ob.DESIGNATION_NAME_EN + " , " + ob.DEPARTMENT_NAME_EN;
            PushNotification push = new PushNotification();
            var obj = new HR_LEAVE_TRANSModel().saveDataForEleave(ob, pOption);
            Hub.Clients.All.OnlineLeaveApproverNotif();
            Hub.Clients.All.OnlineLeaveReqesterNotif();

            //RecurringJob.AddOrUpdate("PushNotificationCorn", () => new PushNotification().PushNotificationCorn(), Cron.Hourly);
            //RecurringJob.Trigger("PushNotificationCorn");

            if (obj.PUSH_REGI_ID != string.Empty)
            {
                push = new PushNotification().PushToAndroidDevice(obj.PUSH_REGI_ID, "eLeave", PushMessage);
                if (push.Message == "Error")
                {
                    //BackgroundJob.Schedule(() => new PushNotification().PushToAndroidDevice(obj.PUSH_REGI_ID, "eLeave", PushMessage), TimeSpan.FromMinutes(2));
                }

            }
            else
            {
                push = new PushNotification()
                {
                    Message = "Approver divice is not registered.Please let him register",
                    Status = false
                };
            }
            obj.Message = push.Message;

            if (pOption == 2003 && ob.IS_CONFIRM_JOIN == "N" && obj.APRV_EMAIL_LIST != string.Empty)
            {
                Hub.Clients.All.updateNotifCount();
                Hub.Clients.All.showNotification();
                Hub.Clients.All.getMessageData();
                BackgroundJob.Enqueue(() => SickLeaveMail(obj.APRV_EMAIL_LIST, ob.EMP_FULL_NAME_EN, ob.DESIGNATION_NAME_EN, ob.DEPARTMENT_NAME_EN));
            }
            else if (pOption == 2003 && ob.IS_CONFIRM_JOIN == "N" && obj.APRV_EMAIL_LIST == string.Empty)
            {
                Hub.Clients.All.updateNotifCount();
                Hub.Clients.All.showNotification();
                Hub.Clients.All.getMessageData();
            }


            return Json(obj, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult ProcessLeaveReset(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, Int64 HR_LEAVE_TYPE_ID)
        {
            string vMsg = "";

            vMsg = new HR_LEAVE_TRANSModel().ProcessLeaveReset(HR_COMPANY_ID, RF_FISCAL_YEAR_ID, HR_LEAVE_TYPE_ID);

            return Json(new { success = true, vMsg });
        }


        public JsonResult LeaveDataById(Int64 HR_LEAVE_TRANS_ID, Int64? HR_LEAVE_APRVL_ID, Int64? SC_USER_ID)
        {

            var obj = new HR_LEAVE_TRANSModel().LeaveDataById(HR_LEAVE_TRANS_ID, HR_LEAVE_APRVL_ID, SC_USER_ID);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult LeaveTransEntry()
        {
            return PartialView();
        }

        public PartialViewResult LeaveTransList()
        {
            return PartialView();
        }

        public PartialViewResult LeaveTransListData()
        {
            return PartialView();
        }
        public PartialViewResult LeaveReset()
        {
            return PartialView();
        }

        public PartialViewResult LeaveResetData()
        {
            return PartialView();
        }


        public JsonResult EmployeeAutoData(Int64? LK_GENDER_ID, string pEMPLOYEE_CODE)
        {
            var employeeData = new HR_EMPLOYEEModel().EmployeeAutoData(LK_GENDER_ID, pEMPLOYEE_CODE);
            return Json(employeeData, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult LeaveTransEdit()
        {
            return PartialView();
        }


        public JsonResult loadLeaveTransData(HR_LEAVE_TRANSModel ob)
        {
            var LeaveTrans = new HR_LEAVE_TRANSModel().loadLeaveTransData(ob);
            return Json(LeaveTrans, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult LeaveTransEditData()
        {
            return PartialView();
        }

        public JsonResult CompanyAllPeriodListData(Int64 pHR_COMPANY_ID, Int64? pHR_PERIOD_TYPE_ID)
        {
            var ob = new ACC_PAY_PERIODModel().AllPeriodListData(pHR_COMPANY_ID, pHR_PERIOD_TYPE_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Notifications(Int64 HR_LEAVE_TRANS_ID)
        {
            var obList = new HR_LEAVE_TRANSModel().Notifications(HR_LEAVE_TRANS_ID);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AttendanceStatus(Int64 HR_EMPLOYEE_ID, DateTime FROM_DATE, DateTime TO_DATE)
        {
            var obList = new HR_LEAVE_TRANSModel().AttendanceStatus(HR_EMPLOYEE_ID, FROM_DATE, TO_DATE);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }



        public PartialViewResult eLeave()
        {
            return PartialView();
        }

        public static void SickLeaveMail(String To, String EMP_FULL_NAME_EN, String DESIGNATION_NAME_EN, String DEPARTMENT_NAME_EN)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            var email = new SickLeaveMailService
            {
                To = To,
                EMP_FULL_NAME_EN = EMP_FULL_NAME_EN,
                DESIGNATION_NAME_EN = DESIGNATION_NAME_EN,
                DEPARTMENT_NAME_EN = DEPARTMENT_NAME_EN,

            };

            emailService.Send(email);
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult ClearFromDashboard(HR_LEAVE_TRANSModel obj, Int64 pOption)
        {
            string vMsg = "";
            try
            {
                vMsg = new HR_LEAVE_TRANSModel().ClearFromDashboard(obj, pOption);

                if (pOption == 2001)
                {

                    Hub.Clients.All.OnlineLeaveReqesterNotif();
                }
                else if (pOption == 2002)
                {
                    Hub.Clients.All.OnlineLeaveApproverNotif();
                }

                return Json(new { success = true, vMsg });
            }
            catch (Exception e)
            {
                return Json(new { success = false, e.Message });
            }
        }

    }
}